using System;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Ateroids
{
    public class AsteroidTrigger : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Asteroid))
                return;
            
            OnTriggered?.Invoke(other);
        }
    }
}