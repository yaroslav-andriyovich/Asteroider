using Code.Infrastructure.Pools.Poolable;
using Code.Services.OutOfBounds.Other.Operations.Base;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Services.OutOfBounds.Components
{
    public class RestrictedMovementProvider : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public MotionRestraintOperation Operation { get; private set; }
        [field: SerializeField] public bool IsSpawnedOutside { get; private set; }

        public bool IsPoolable => PoolableRef != null;
        public IPoolable PoolableRef { get; private set; }
        
        private RestraintMotionService _restraintMotionService;

        private void Awake()
        {
            if (TryGetComponent(out IPoolable poolable))
                PoolableRef = poolable;
        }

        private void OnEnable() => 
            _restraintMotionService.Register(this);

        private void OnDisable() => 
            _restraintMotionService.UnRegister(this);

        [Inject]
        public void Construct(RestraintMotionService restraintMotionService) => 
            _restraintMotionService = restraintMotionService;
    }
}