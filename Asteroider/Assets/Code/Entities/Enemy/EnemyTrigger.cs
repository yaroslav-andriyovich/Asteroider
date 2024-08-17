using System;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        public event Action<Collider> OnTrigger;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.PlayableZone) || other.CompareTag(GameTags.Asteroid))
                return;
            
            OnTrigger?.Invoke(other);
        }
    }
}