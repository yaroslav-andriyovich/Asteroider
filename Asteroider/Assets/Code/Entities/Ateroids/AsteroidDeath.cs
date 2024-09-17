using System;
using Code.Effects;
using Code.Entities.Components.Death;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Entities.Ateroids
{
    public class AsteroidDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private AsteroidTrigger _trigger;
        
        public event Action OnHappened;
        
        private MonoPool<ExplosionEffect> _explosionEffectsPool;
        private bool _isDead;

        private void Awake() => 
            _trigger.OnTriggered += OnTriggered;

        private void OnEnable() => 
            _isDead = false;

        private void OnDestroy() => 
            _trigger.OnTriggered -= OnTriggered;

        [Inject]
        public void Construct(PoolService poolService) => 
            _explosionEffectsPool = poolService.GetPool<ExplosionEffect>();

        private void OnTriggered(Collider2D other)
        {
            if (!_isDead)
                Die();
        }

        public void Die()
        {
            _isDead = true;
            PlayEffect();
            OnHappened?.Invoke();
        }

        private void PlayEffect()
        {
            ExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);

            effect.Play();
        }
    }
}