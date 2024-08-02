using Code.Effects;
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

        private MonoPool<AsteroidExplosionEffect> _explosionEffectsPool;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;

            AsteroidExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);
            
            effect.Play();
            Release();
        }

        [Inject]
        public void Construct(PoolService poolService) => 
            _explosionEffectsPool = poolService.GetPool<AsteroidExplosionEffect>();
    }
}