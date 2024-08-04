using Code.Effects;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.LazerBullets
{
    public class LazerBullet : PoolableBase<LazerBullet>
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }

        private MonoPool<AsteroidExplosionEffect> _explosionEffectsPool;

        [Inject]
        public void Construct(PoolService poolService) =>
            _explosionEffectsPool = poolService.GetPool<AsteroidExplosionEffect>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.PlayableZone))
                return;

            if (!other.CompareTag(GameTags.Asteroid))
            {
                AsteroidExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);

                effect.Play();
            }
            
            Release();
        }
    }
}