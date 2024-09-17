using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Ateroids
{
    public class AsteroidAttack : MonoBehaviour
    {
        [SerializeField] private AsteroidTrigger _trigger;
        [SerializeField] private float _damage;

        private void Awake() => 
            _trigger.OnTriggered += OnTrigger;

        private void OnDestroy() => 
            _trigger.OnTriggered -= OnTrigger;

        private void OnTrigger(Collider2D other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                ApplyDamage(damageable);
        }
        
        private void ApplyDamage(IDamageable damageable) => 
            damageable.TakeDamage(_damage);
    }
}