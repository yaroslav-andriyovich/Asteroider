using System;
using Code.Effects;
using Code.Entities.Death;
using Code.Entities.HealthPoints;
using Code.Infrastructure.Pools;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Enemy
{
    public class EnemyDeath : MonoBehaviour, IDeath
    {
        [SerializeField] private EnemyTrigger _trigger;
        
        public event Action OnHappened;
        
        private IHealth _health;
        private MonoPool<ExplosionEffect> _explosionEffectsPool;
        private bool _isDead;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            
            _health.OnChanged += OnHealthChanged;
            _trigger.OnTrigger += OnTrigger;
        }

        private void OnEnable() => 
            _isDead = false;
        
        private void OnTrigger(Collider2D other)
        {
            if (!_isDead 
                && (other.CompareTag(GameTags.Player) 
                || other.CompareTag(GameTags.Obstacle)
                || other.CompareTag(GameTags.Enemy)))
                Die();
        }

        private void OnDestroy()
        {
            _health.OnChanged -= OnHealthChanged;
            _trigger.OnTrigger -= OnTrigger;
        }

        [Inject]
        public void Construct(PoolService poolService) => 
            _explosionEffectsPool = poolService.GetPool<ExplosionEffect>();

        public void Die()
        {
            _isDead = true;
            Vector3 position = transform.position;
            ExplosionEffect effect = _explosionEffectsPool.Get(position, Quaternion.identity);

            effect.Play();
            OnHappened?.Invoke();
        }

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }
    }
}