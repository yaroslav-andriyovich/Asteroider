using System;
using Code.Effects;
using Code.Entities.Damageables;
using Code.Entities.Death;
using Code.Infrastructure.Pools;
using Code.Infrastructure.Pools.Poolable;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Weapons.Bullets
{
    public class LaserBullet : PoolableBase<LaserBullet>, IDeath
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        [SerializeField] private float _damage;

        public event Action OnHappened;
        private MonoPool<ExplosionEffect> _explosionEffectsPool;

        [Inject]
        public void Construct(PoolService poolService) =>
            _explosionEffectsPool = poolService.GetPool<ExplosionEffect>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(GameTags.Asteroid))
            {
                if (other.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_damage);
                
                ExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);

                effect.Play();
            }
            
            Release();
        }

        public void Die()
        {
            ExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);

            effect.Play();
            Release();
        }
    }
}