using System;
using Code.Entities.Components;
using UnityEngine;

namespace Code.Entities.Player
{
    public class PlayerHealth : MonoBehaviour, IHealth
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

        private void Start() => 
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