using System;
using Code.Entities.Damageables;
using Code.Entities.HealthPoints;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyHealth : MonoBehaviour, IHealth, IDamageable
    {
        [field: SerializeField] public float Max { get; private set; }

        public event Action OnChanged;
        public float Current
        {
            get => _current;
            set
            {
                _current = value;
                OnChanged?.Invoke();
            }
        }

        private float _current;

        private void OnEnable() => 
            Restore();

        public void TakeDamage(float damage)
        {
            if (damage < 0)
                throw new InvalidOperationException("Damage value < 0!");

            Current = Mathf.Max(0, Current - damage);
        }

        public void Restore() => 
            Current = Max;
    }
}