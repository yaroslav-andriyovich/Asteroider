using Code.Entities.Components;
using Code.UI;
using UnityEngine;

namespace Code.Entities.Player
{
    public class PlayerDamage : MonoBehaviour, IDamagable
    {
        [SerializeField] private EntityProgressBar _healthBar;
        [SerializeField] private EntityProgressBar _shieldBar;

        private IHealth _health;
        private IShield _shield;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _shield = GetComponent<IShield>();
            
            _health.OnChanged += OnHealthChanged;
            _shield.OnChanged += OnShieldChanged;
        }

        private void OnDestroy()
        {
            _health.OnChanged -= OnHealthChanged;
            _shield.OnChanged -= OnShieldChanged;
        }
        
        public void TakeDamage(float damage)
        {
            if (!_shield.TryAbsorbDamage(damage, out float remainingDamage))
                _health.TakeDamage(remainingDamage);
        }

        private void OnHealthChanged() => 
            _healthBar.SetValue(_health.Current, _health.Max);
        
        private void OnShieldChanged()
        {
            _shieldBar.SetValue(_shield.Current, _shield.Max);
            
            if (_shield.Current <= 0f)
                _shieldBar.gameObject.SetActive(false);
            else if (!_shieldBar.isActiveAndEnabled)
                _shieldBar.gameObject.SetActive(true);
        }
    }
}