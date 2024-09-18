using Code.Entities.Damageables;
using Code.Entities.HealthPoints;
using Code.Entities.Shields;
using UnityEngine;

namespace Code.Entities.Player
{
    public class PlayerDamageReceiver : MonoBehaviour, IDamageable
    {
        private IHealth _health;
        private IShield _shield;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _shield = GetComponent<IShield>();
        }
        
        public void TakeDamage(float damage)
        {
            if (!_shield.TryAbsorbDamage(damage, out float remainingDamage))
                _health.TakeDamage(remainingDamage);
        }
    }
}