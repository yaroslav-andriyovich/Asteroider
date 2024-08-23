using System;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Ateroids
{
    public class AsteroidTrigger : MonoBehaviour
    {
        public event Action<Collider> OnTriggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.PlayableZone) || other.CompareTag(GameTags.Asteroid))
                return;
            
            OnTriggered?.Invoke(other);
        }
    }
}