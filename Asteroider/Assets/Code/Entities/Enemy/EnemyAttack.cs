using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyTrigger _trigger;
        [SerializeField] private float _collisionDamage;
        
        private void Awake() => 
            _trigger.OnTrigger += OnTrigger;

        private void OnDestroy() => 
            _trigger.OnTrigger -= OnTrigger;
        
        private void OnTrigger(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damagable))
                damagable.TakeDamage(_collisionDamage);
        }
    }
}