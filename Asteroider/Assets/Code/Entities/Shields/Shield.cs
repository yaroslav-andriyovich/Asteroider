using System;
using UnityEngine;

namespace Code.Entities.Shields
{
    public class Shield : MonoBehaviour, IShield
    {
        [field: SerializeField] public float Max { get; private set; }

        [SerializeField] private bool _activeOnAwake;

        public event Action OnChanged;
        public float Current
        {
            get => _current;
            private set
            {
                _current = value;
                OnChanged?.Invoke();
            }
        }

        private float _current;

        private void Start() => 
            Current = _activeOnAwake ? Max : 0;

        public bool TryAbsorbDamage(float damage, out float remainingDamage)
        {
            if (damage < 0)
                throw new InvalidOperationException("Damage value < 0!");
            
            float shieldAfterDamage = Current - damage;

            if (Current > 0)
                Current = Mathf.Max(0, shieldAfterDamage);

            remainingDamage = shieldAfterDamage < 0 ? shieldAfterDamage * -1 : 0;

            return remainingDamage == 0;
        }

        public void Restore() => 
            Current = Max;
    }
}