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
        
        private void OnTrigger(Collider other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_collisionDamage);
        }
    }
}