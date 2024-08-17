using Code.Entities.Components;
using Code.UI;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyUI : MonoBehaviour
    {
        [SerializeField] private EntityProgressBar _healthBar;
        
        private IHealth _health;
        
        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _health.OnChanged += OnHealthChanged;
        }

        private void OnDestroy() => 
            _health.OnChanged -= OnHealthChanged;

        private void OnHealthChanged() => 
            _healthBar.SetValue(_health.Current, _health.Max);
    }
}