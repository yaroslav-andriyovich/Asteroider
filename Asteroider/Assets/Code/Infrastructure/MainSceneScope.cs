using Code.Effects;
using Code.Entities;
using Code.Entities.Ateroids;
using Code.Entities.Player;
using Code.ObjectEmitting;
using Code.Services.Pools;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class MainSceneScope : LifetimeScope
    {
        [Header("Player")]
        [SerializeField] private PlayerShip _playerShip;
        
        [Header("Asteroids")]
        [SerializeField] private Asteroid _asteroid1Prefab;
        [SerializeField] private Asteroid _asteroid2Prefab;
        [SerializeField] private Asteroid _asteroid3Prefab;

        [Header("Effects")]
        [SerializeField] private ExplosionAudioEffect _explosionAudioEffect;
        [SerializeField] private AsteroidExplosionEffect _prefabExplosionEffect;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PoolService>(Lifetime.Singleton);
            builder.RegisterComponent(_explosionAudioEffect);
            builder.RegisterComponentInHierarchy<ObjectEmittingZone>();

            builder.RegisterComponent(_playerShip);

            builder.RegisterBuildCallback(resolver =>
            {
                PoolService poolService = resolver.Resolve<PoolService>();
                
                IPoolableFactory<AsteroidExplosionEffect> explosionEffectFactory = new PoolableFactory<AsteroidExplosionEffect>(resolver, _prefabExplosionEffect);
                IPoolableFactory<Asteroid> asteroid1Factory = new PoolableFactory<Asteroid>(resolver, _asteroid1Prefab);
                IPoolableFactory<Asteroid> asteroid2Factory = new PoolableFactory<Asteroid>(resolver, _asteroid2Prefab);
                IPoolableFactory<Asteroid> asteroid3Factory = new PoolableFactory<Asteroid>(resolver, _asteroid3Prefab);

                poolService.CreatePool(explosionEffectFactory, 24);
                poolService.CreatePool(asteroid1Factory, 8);
                poolService.CreatePool(asteroid2Factory, 8);
                poolService.CreatePool(asteroid3Factory, 8);
            });
        }
    }
}