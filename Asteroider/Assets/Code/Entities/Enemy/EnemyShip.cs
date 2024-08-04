using Code.Effects;
using Code.Entities.LazerBullets;
using Code.Entities.Player;
using Code.Infrastructure;
using Code.ObjectEmitting;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Enemy
{
    public class EnemyShip : PoolableBase<EnemyShip>, ICoroutineRunner, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private LazerBullet _bulletPrefab;
        [SerializeField] private ShipGun _gun;

        private MonoPool<AsteroidExplosionEffect> _explosionEffectsPool;

        private void OnDisable() => 
            _gun.Deactivate();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameTags.PlayableZone) || other.CompareTag(GameTags.Asteroid))
                return;

            if (other.CompareTag(GameTags.Player))
            {
                AsteroidExplosionEffect effect = _explosionEffectsPool.Get(transform.position, Quaternion.identity);
            
                effect.Play();
            }
            
            Release();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(GameTags.PlayableZone))
                return;

            Vector3 velocity = Rigidbody.velocity;

            velocity.x *= -1;

            Rigidbody.velocity = velocity;
        }

        [Inject]
        public void Construct(PoolService poolService)
        {
            _explosionEffectsPool = poolService.GetPool<AsteroidExplosionEffect>();
            
            MonoPool<LazerBullet> bulletsPool = poolService.GetPool(_bulletPrefab);
            
            _gun.Initialize(bulletsPool, this);
        }

        public void Emit() => 
            _gun.Activate();
    }
}