using Code.Effects;
using Code.Entities.Components;
using Code.ObjectEmitting;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Ateroids
{
    public class Asteroid : PoolableBase<Asteroid>, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private float _damage;

        private MonoPool<ExplosionEffect> _explosionEffectsPool;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.PlayableZone) || other.CompareTag(GameTags.Asteroid))
                return;

            if (other.TryGetComponent(out IDamagable damagable))
                damagable.TakeDamage(_damage);

            ExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);
            
            effect.Play();
            Release();
        }

        [Inject]
        public void Construct(PoolService poolService) => 
            _explosionEffectsPool = poolService.GetPool<ExplosionEffect>();

        public void Emit()
        {
        }
    }
}