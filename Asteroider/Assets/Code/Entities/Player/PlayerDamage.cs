using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Player
{
    public class PlayerDamage : MonoBehaviour, IDamagable
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