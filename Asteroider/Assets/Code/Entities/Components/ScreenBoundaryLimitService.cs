using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Entities.Components
{
    public class ScreenBoundaryLimitService : MonoBehaviour
    {
        [SerializeField] private float _autoCleaningDelay;
        
        private readonly List<LimitedScreenMotionProvider> _limitedObjects = new List<LimitedScreenMotionProvider>();
        private readonly Dictionary<LimitedScreenMotionProvider, float> _forAutoCleaning = new Dictionary<LimitedScreenMotionProvider, float>();
        private Vector3 _screenBounds;
        
        private void Awake()
        {
            Camera mainCamera = Camera.main;
            
            _screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        }

        private void FixedUpdate()
        {
            for (int index = 0; index < _limitedObjects.Count; index++)
            {
                LimitedScreenMotionProvider limited = _limitedObjects[index];
                
                Check(limited);
            }
        }

        public void Register(LimitedScreenMotionProvider limited)
        {
            if (_limitedObjects.Contains(limited))
                throw new InvalidOperationException($"Limited({limited.name}) already registered!");
            
            _limitedObjects.Add(limited);

            RegisterForAutoCleaning(limited);
        }

        public void UnRegister(LimitedScreenMotionProvider limited)
        {
            if (!_limitedObjects.Contains(limited))
                throw new InvalidOperationException($"Limited({limited.name}) not registered!");

            _limitedObjects.Remove(limited);

            UnRegisterFromAutoCleaning(limited);
        }

        private void RegisterForAutoCleaning(LimitedScreenMotionProvider limited)
        {
            OutOfBoundaryResult outOfBoundaryResult = CheckOutOfBoundary(limited.Rigidbody.position);

            if (outOfBoundaryResult.IsOutside())
            {
                float registeredTime = Time.time;
                
                _forAutoCleaning.Add(limited, registeredTime);
            }
        }
        
        private void UnRegisterFromAutoCleaning(LimitedScreenMotionProvider limited)
        {
            if (_forAutoCleaning.ContainsKey(limited))
                _forAutoCleaning.Remove(limited);
        }

        private void Check(LimitedScreenMotionProvider limited)
        {
            if (CanSkipCheck(limited))
                return;

            OutOfBoundaryResult outOfBoundaryResult = CheckOutOfBoundary(limited.Rigidbody.position);
            
            if (outOfBoundaryResult.IsInside())
            {
                UnRegisterFromAutoCleaning(limited);
                return;
            }

            if (IsObjectAwaitingCleanup(limited, out float registeredTime))
            {
                ClearObjectIfTimeIsUp(limited, registeredTime);
                return;
            }

            ApplyOperation(limited, outOfBoundaryResult);
        }

        private bool CanSkipCheck(LimitedScreenMotionProvider limited) => 
            limited.Operation == ScreenLimitOperation.None;

        private OutOfBoundaryResult CheckOutOfBoundary(Vector2 position)
        {
            bool isOutOfHorizontal = position.x < -_screenBounds.x || position.x > _screenBounds.x;
            bool isOutOfVertical = position.y < -_screenBounds.y || position.y > _screenBounds.y;

            return new OutOfBoundaryResult()
            {
                isOutOfHorizontal = isOutOfHorizontal,
                isOutOfVertical = isOutOfVertical
            };
        }

        private bool IsObjectAwaitingCleanup(LimitedScreenMotionProvider limited, out float registeredTime) => 
            _forAutoCleaning.TryGetValue(limited, out registeredTime);

        private void ClearObjectIfTimeIsUp(LimitedScreenMotionProvider limited, float registeredTime)
        {
            if (IsTimeToClear(registeredTime))
                Clear(limited);
        }

        private bool IsTimeToClear(float registeredTime) => 
            Time.time - registeredTime >= _autoCleaningDelay;

        private void ApplyOperation(LimitedScreenMotionProvider limited, OutOfBoundaryResult outOfBoundaryResult)
        {
            switch (limited.Operation)
            {
                case ScreenLimitOperation.LimitMotion:
                    LimitMotion(limited);
                    break;

                case ScreenLimitOperation.LimitYAndInfiniteXMotion:
                    LimitYAndInfiniteXMotion(limited);
                    break;

                case ScreenLimitOperation.InvertXMotionOrClear:
                    if (outOfBoundaryResult.IsOutOnlyOfHorizontal())
                        InvertXMotionOrClear(limited);
                    else
                        Clear(limited);
                    break;

                case ScreenLimitOperation.Clear:
                    Clear(limited);
                    break;

                default:
                    throw new InvalidOperationException($"Unknown operation: {limited.Operation}");
            }
        }

        private void LimitMotion(LimitedScreenMotionProvider limited)
        {
            Vector2 position = limited.Rigidbody.position;

            position.x = Mathf.Clamp(position.x, _screenBounds.x * -1f, _screenBounds.x);
            position.y = Mathf.Clamp(position.y, _screenBounds.y * -1f, _screenBounds.y);

            limited.Rigidbody.position = position;
        }

        private void LimitYAndInfiniteXMotion(LimitedScreenMotionProvider limited)
        {
            Vector2 position = limited.Rigidbody.position;

            if (position.x >= _screenBounds.x)
                position.x = _screenBounds.x * -1f;
            else if (position.x <= _screenBounds.x * -1f)
                position.x = _screenBounds.x;
            
            position.y = Mathf.Clamp(position.y, _screenBounds.y * -1f, _screenBounds.y);

            limited.Rigidbody.position = position;
        }

        private void InvertXMotionOrClear(LimitedScreenMotionProvider limited)
        {
            Vector3 velocity = limited.Rigidbody.velocity;
            
            velocity.x *= -1f;

            limited.Rigidbody.velocity = velocity;
        }

        private void Clear(LimitedScreenMotionProvider limited)
        {
            if (limited.IsPoolable)
                limited.PoolableRef.Release();
            else
                Destroy(limited.gameObject);
        }

        private struct OutOfBoundaryResult
        {
            public bool isOutOfHorizontal;
            public bool isOutOfVertical;

            public bool IsInside() => 
                !isOutOfHorizontal && !isOutOfVertical;
            
            public bool IsOutside() => 
                isOutOfHorizontal || isOutOfVertical;

            public bool IsOutOnlyOfHorizontal() => 
                isOutOfHorizontal && !isOutOfVertical;
        }
    }
}