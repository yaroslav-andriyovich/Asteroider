using System;
using Code.Effects;
using Code.Entities.Components;
using Code.Entities.Components.Death;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerDeath : MonoBehaviour, IDeath
    {
        public event Action OnHappened;
        
        private IHealth _health;
        private MonoPool<ExplosionEffect> _explosionEffectsPool;
        private bool _isDead;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            
            _health.OnChanged += OnHealthChanged;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameTags.Obstacle))
                Die();
        }
        
        private void OnDestroy() => 
            _health.OnChanged -= OnHealthChanged;

        [Inject]
        public void Construct(PoolService poolService) => 
            _explosionEffectsPool = poolService.GetPool<ExplosionEffect>();

        public void Die()
        {
            _isDead = true;
            ExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);

            effect.Play();
            gameObject.SetActive(false);
            OnHappened?.Invoke();
        }

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }
    }
}