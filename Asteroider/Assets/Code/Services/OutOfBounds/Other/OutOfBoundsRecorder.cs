using System.Collections.Generic;
using Code.Services.OutOfBounds.Components;
using UnityEngine;

namespace Code.Services.OutOfBounds.Other
{
    public class OutOfBoundsRecorder
    {
        private readonly Dictionary<RestrictedMovementProvider, float> _spawnedOutOfBounds = new Dictionary<RestrictedMovementProvider, float>();

        public void TryRegister(RestrictedMovementProvider restricted)
        {
            if (restricted.IsSpawnedOutside)
            {
                float registeredTime = Time.time;
                
                _spawnedOutOfBounds.Add(restricted, registeredTime);
            }
        }

        public void UnRegister(RestrictedMovementProvider restricted) => 
            _spawnedOutOfBounds.Remove(restricted);

        public bool IsObjectAwaitingCleanup(RestrictedMovementProvider restricted, out float registeredTime) => 
            _spawnedOutOfBounds.TryGetValue(restricted, out registeredTime);
    }
}