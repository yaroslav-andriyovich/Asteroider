using Code.Services.OutOfBounds.Components;
using UnityEngine;

namespace Code.Services.OutOfBounds.Other
{
    public class RestrictedCleaner
    {
        private readonly float _autoCleaningDelay;
        
        public RestrictedCleaner(float autoCleaningDelay) => 
            _autoCleaningDelay = autoCleaningDelay;

        public void ClearObjectIfTimeIsUp(RestrictedMovementProvider restricted, float registeredTime)
        {
            if (IsTimeToClear(registeredTime))
                Clear(restricted);
        }

        public bool IsTimeToClear(float registeredTime) => 
            Time.time - registeredTime >= _autoCleaningDelay;

        public void Clear(RestrictedMovementProvider restricted)
        {
            if (restricted.IsPoolable)
                restricted.PoolableRef.Release();
            else
                Object.Destroy(restricted.gameObject);
        }
    }
}