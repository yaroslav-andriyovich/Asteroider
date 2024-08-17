using Code.Effects;
using Code.Entities.Components;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Entities.Enemy
{
    public class EnemyDeath : MonoBehaviour
    {
        private IPoolable<EnemyShip> _enemy;
        private IHealth _health;
        private MonoPool<ExplosionEffect> _explosionEffectsPool;
        private bool _isDead;

        private void Awake()
        {
            _enemy = GetComponent<IPoolable<EnemyShip>>();
            _health = GetComponent<IHealth>();
            
            _health.OnChanged += OnHealthChanged;
        }

        private void OnEnable() => 
            _isDead = false;

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
            _enemy.Release();
        }

        private void OnHealthChanged()
        {
            if (!_isDead && _health.Current <= 0)
                Die();
        }
    }
}