using System;
using Code.Effects;
using Code.Entities.Ateroids.Poolable;
using Code.Entities.Enemy;
using Code.Entities.Obstacles;
using Code.Entities.Weapons.Bullets;
using Code.Infrastructure.Pools.Poolable.Factory;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Scene
{
    [Serializable]
    public class MainScenePoolInitializer
    {
        [Header("Player")]
        [SerializeField] private LaserBullet _playerPrimaryBullet;
        [SerializeField] private LaserBullet _playerSecondaryBullet;
        
        [Header("Enemy")]
        [SerializeField] private EnemyShip _enemyShip;
        [SerializeField] private LaserBullet _enemyBullet;
        
        [Header("Asteroids")]
        [SerializeField] private PoolableAsteroid _asteroid1Prefab;
        [SerializeField] private PoolableAsteroid _asteroid2Prefab;
        [SerializeField] private PoolableAsteroid _asteroid3Prefab;

        [Header("Obstacles")]
        [SerializeField] private Obstacle _bigAsteroidObstaclePrefab;

        [Header("Effects")]
        [SerializeField] private ExplosionEffect _prefabExplosionEffect;

        public void Initialize(IObjectResolver resolver)
        {
            PoolService poolService = resolver.Resolve<PoolService>();

            CreatePoolData[] createPoolDatas = new[]
            {
                new CreatePoolData(typeof(ExplosionEffect), _prefabExplosionEffect, 24),
                new CreatePoolData(typeof(PoolableAsteroid), _asteroid1Prefab, 8),
                new CreatePoolData(typeof(PoolableAsteroid), _asteroid2Prefab, 8),
                new CreatePoolData(typeof(PoolableAsteroid), _asteroid3Prefab, 8),
                new CreatePoolData(typeof(LaserBullet), _playerPrimaryBullet, 4),
                new CreatePoolData(typeof(LaserBullet), _playerSecondaryBullet, 8),
                new CreatePoolData(typeof(LaserBullet), _enemyBullet, 8),
                new CreatePoolData(typeof(EnemyShip), _enemyShip, 2),
                new CreatePoolData(typeof(Obstacle), _bigAsteroidObstaclePrefab, 1),
            };
            
            IPoolableFactory<ExplosionEffect> explosionEffectFactory = new PoolableFactory<ExplosionEffect>(resolver, _prefabExplosionEffect);
            IPoolableFactory<PoolableAsteroid> asteroid1Factory = new PoolableFactory<PoolableAsteroid>(resolver, _asteroid1Prefab);
            IPoolableFactory<PoolableAsteroid> asteroid2Factory = new PoolableFactory<PoolableAsteroid>(resolver, _asteroid2Prefab);
            IPoolableFactory<PoolableAsteroid> asteroid3Factory = new PoolableFactory<PoolableAsteroid>(resolver, _asteroid3Prefab);
                
            IPoolableFactory<LaserBullet> playerPrimaryBulletFactory = new PoolableFactory<LaserBullet>(resolver, _playerPrimaryBullet);
            IPoolableFactory<LaserBullet> playerSecondaryBulletFactory = new PoolableFactory<LaserBullet>(resolver, _playerSecondaryBullet);
            IPoolableFactory<LaserBullet> enemyBulletFactory = new PoolableFactory<LaserBullet>(resolver, _enemyBullet);
            IPoolableFactory<EnemyShip> enemyShipFactory = new PoolableFactory<EnemyShip>(resolver, _enemyShip);
                
            IPoolableFactory<Obstacle> bigAsteroidObstacleFactory = new PoolableFactory<Obstacle>(resolver, _bigAsteroidObstaclePrefab);

            poolService.CreatePool(explosionEffectFactory, 24);
            poolService.CreatePool(asteroid1Factory, 8);
            poolService.CreatePool(asteroid2Factory, 8);
            poolService.CreatePool(asteroid3Factory, 8);
                
            poolService.CreatePool(playerPrimaryBulletFactory, 4);
            poolService.CreatePool(playerSecondaryBulletFactory, 8);
                
            poolService.CreatePool(enemyBulletFactory, 8);
            poolService.CreatePool(enemyShipFactory, 2);
                
            poolService.CreatePool(bigAsteroidObstacleFactory, 1);
        }

        private class CreatePoolData
        {
            public readonly Type type;
            public readonly object prefab;
            public readonly int poolSize;

            public CreatePoolData(Type type, object prefab, int poolSize)
            {
                this.type = type;
                this.prefab = prefab;
                this.poolSize = poolSize;
            }
        }
    }
}