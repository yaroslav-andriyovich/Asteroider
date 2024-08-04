using Code.Effects;
using Code.Entities.Ateroids;
using Code.Entities.Enemy;
using Code.Entities.LazerBullets;
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
        [SerializeField] private LazerBullet _playerPrimaryBullet;
        [SerializeField] private LazerBullet _playerSecondaryBullet;
        
        [Header("Enemy")]
        [SerializeField] private EnemyShip _enemyShip;
        [SerializeField] private LazerBullet _enemyBullet;
        
        [Header("Asteroids")]
        [SerializeField] private Asteroid _asteroid1Prefab;
        [SerializeField] private Asteroid _asteroid2Prefab;
        [SerializeField] private Asteroid _asteroid3Prefab;

        [Header("Effects")]
        [SerializeField] private ExplosionAudio _explosionAudio;
        [SerializeField] private AsteroidExplosionEffect _prefabExplosionEffect;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PoolService>(Lifetime.Singleton);
            builder.RegisterComponent(_explosionAudio);
            builder.RegisterComponentInHierarchy<ObjectEmittingZone>();

            builder.RegisterComponent(_playerShip);

            builder.RegisterBuildCallback(resolver =>
            {
                PoolService poolService = resolver.Resolve<PoolService>();
                
                IPoolableFactory<AsteroidExplosionEffect> explosionEffectFactory = new PoolableFactory<AsteroidExplosionEffect>(resolver, _prefabExplosionEffect);
                IPoolableFactory<Asteroid> asteroid1Factory = new PoolableFactory<Asteroid>(resolver, _asteroid1Prefab);
                IPoolableFactory<Asteroid> asteroid2Factory = new PoolableFactory<Asteroid>(resolver, _asteroid2Prefab);
                IPoolableFactory<Asteroid> asteroid3Factory = new PoolableFactory<Asteroid>(resolver, _asteroid3Prefab);
                
                IPoolableFactory<LazerBullet> playerPrimaryBulletFactory = new PoolableFactory<LazerBullet>(resolver, _playerPrimaryBullet);
                IPoolableFactory<LazerBullet> playerSecondaryBulletFactory = new PoolableFactory<LazerBullet>(resolver, _playerSecondaryBullet);
                IPoolableFactory<LazerBullet> enemyBulletFactory = new PoolableFactory<LazerBullet>(resolver, _enemyBullet);
                IPoolableFactory<EnemyShip> enemyShipFactory = new PoolableFactory<EnemyShip>(resolver, _enemyShip);

                poolService.CreatePool(explosionEffectFactory, 24);
                poolService.CreatePool(asteroid1Factory, 8);
                poolService.CreatePool(asteroid2Factory, 8);
                poolService.CreatePool(asteroid3Factory, 8);
                
                poolService.CreatePool(playerPrimaryBulletFactory, 4);
                poolService.CreatePool(playerSecondaryBulletFactory, 8);
                poolService.CreatePool(enemyBulletFactory, 8);
                poolService.CreatePool(enemyShipFactory, 2);
            });
        }
    }
}