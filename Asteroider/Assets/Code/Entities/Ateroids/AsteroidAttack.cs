using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Ateroids
{
    public class AsteroidAttack : MonoBehaviour
    {
        [SerializeField] private AsteroidTrigger _trigger;
        [SerializeField] private float _damage;

        private void Awake() => 
            _trigger.OnTriggered += ApplyDamage;

        private void OnDestroy() => 
            _trigger.OnTriggered -= ApplyDamage;

        private void ApplyDamage(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }
    }
}