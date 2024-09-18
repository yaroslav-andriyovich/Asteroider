using System;
using Code.Utils;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyTrigger : MonoBehaviour
    {
        public event Action<Collider2D> OnTrigger;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Asteroid))
                return;
            
            OnTrigger?.Invoke(other);
        }
    }
}