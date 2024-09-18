using System;
using System.Collections.Generic;
using Code.Services.OutOfBounds.Components;
using Code.Services.OutOfBounds.Other;
using Code.Services.OutOfBounds.Other.Operations;
using Code.Services.OutOfBounds.Other.Operations.Base;
using Code.Services.WorldBounds;
using UnityEngine;
using VContainer;

namespace Code.Services.OutOfBounds
{
    public class RestraintMotionService : MonoBehaviour
    {
        [SerializeField] private float _autoCleaningDelay;

        private readonly List<RestrictedMovementProvider> _lazyRegisterObjects = new List<RestrictedMovementProvider>();
        private readonly List<RestrictedMovementProvider> _restrictedObjects = new List<RestrictedMovementProvider>();
        
        private WoldBoundsService _woldBoundsService;
        private OutOfBoundsRecorder _outOfBoundsRecorder;
        private RestrictedCleaner _cleaner;
        private RestrictionOperator _operator;

        private void Update()
        {
            for (int index = 0; index < _lazyRegisterObjects.Count; index++)
            {
                RestrictedMovementProvider restricted = _lazyRegisterObjects[index];

                ProcessLazyRegister(restricted);
            }
        }

        private void FixedUpdate()
        {
            for (int index = 0; index < _restrictedObjects.Count; index++)
            {
                RestrictedMovementProvider restricted = _restrictedObjects[index];
                
                ProcessObject(restricted);
            }
        }

        [Inject]
        public void Construct(WoldBoundsService woldBoundsService)
        {
            _woldBoundsService = woldBoundsService;
            
            _outOfBoundsRecorder = new OutOfBoundsRecorder();
            _cleaner = new RestrictedCleaner(_autoCleaningDelay);
            _operator = new RestrictionOperator(new Dictionary<MotionRestraintOperation, IMotionRestrictionStrategy>
            {
                { MotionRestraintOperation.LimitInsideBounds, new LimitInsideBounds(_woldBoundsService.Bounds) },
                { MotionRestraintOperation.LimitYAndInfiniteX, new LimitYAndInfiniteX(_woldBoundsService.Bounds) },
                { MotionRestraintOperation.InvertXOrClear, new InvertXOrClear(_woldBoundsService, _cleaner) },
                { MotionRestraintOperation.Clear, new ClearObjectOperation(_cleaner) },
                { MotionRestraintOperation.InfiniteXOrClear, new InfiniteXOrClear(_woldBoundsService, _cleaner) }
            });
        }

        public void Register(RestrictedMovementProvider restricted)
        {
            if (_lazyRegisterObjects.Contains(restricted))
                throw new InvalidOperationException($"Restricted({restricted.name}) already registered!");
            
            _lazyRegisterObjects.Add(restricted);
        }

        public void UnRegister(RestrictedMovementProvider restricted)
        {
            _lazyRegisterObjects.Remove(restricted);
            _restrictedObjects.Remove(restricted);
            _outOfBoundsRecorder.UnRegister(restricted);
        }

        private void ProcessLazyRegister(RestrictedMovementProvider restricted)
        {
            _restrictedObjects.Add(restricted);
            _outOfBoundsRecorder.TryRegister(restricted);
            _lazyRegisterObjects.Remove(restricted);
        }

        private void ProcessObject(RestrictedMovementProvider restricted)
        {
            if (CanSkipCheck(restricted))
                return;

            OutOfBoundsResult outOfBoundsResult = _woldBoundsService.CheckOutOfBounds(restricted.Rigidbody.position);
            
            if (IsObjectInsideBounds(outOfBoundsResult))
            {
                _outOfBoundsRecorder.UnRegister(restricted);
                return;
            }

            if (_outOfBoundsRecorder.IsObjectAwaitingCleanup(restricted, out float registeredTime))
            {
                _cleaner.ClearObjectIfTimeIsUp(restricted, registeredTime);
                return;
            }

            _operator.ApplyRestriction(restricted);
        }
        
        private bool CanSkipCheck(RestrictedMovementProvider restricted) => 
            restricted.Operation == MotionRestraintOperation.None;
        
        private bool IsObjectInsideBounds(OutOfBoundsResult outOfBoundsResult) => 
            outOfBoundsResult.IsInside();
    }
}