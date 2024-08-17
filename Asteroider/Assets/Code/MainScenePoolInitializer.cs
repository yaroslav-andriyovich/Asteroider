using System;
using Code.Effects;
using Code.Entities.Ateroids;
using Code.Entities.Enemy;
using Code.Entities.LazerBullets;
using Code.Entities.Obstacles;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code
{
    [Serializable]
    public class MainScenePoolInitializer
    {
        [Header("Player")]
        [SerializeField] private LazerBullet _playerPrimaryBullet;
        [SerializeField] private LazerBullet _playerSecondaryBullet;
        
        [Header("Enemy")]
        [SerializeField] private EnemyShip _enemyShip;
        [SerializeField] private LazerBullet _enemyBullet;
        
        [Header("Asteroids")]
        [SerializeField] private Asteroid _asteroid1Prefab;
        [SerializeField] private Asteroid _asteroid2Prefab;
        [SerializeField] private Asteroid _asteroid3Prefab;

        [Header("Obstacles")]
        [SerializeField] private Obstacle _bigAsteroidObstaclePrefab;

        [Header("Effects")]
        [SerializeField] private ExplosionEffect _prefabExplosionEffect;

        public void Initialize(IObjectResolver resolver)
        {
            PoolService poolService = resolver.Resolve<PoolService>();
                
            IPoolableFactory<ExplosionEffect> explosionEffectFactory = new PoolableFactory<ExplosionEffect>(resolver, _prefabExplosionEffect);
            IPoolableFactory<Asteroid> asteroid1Factory = new PoolableFactory<Asteroid>(resolver, _asteroid1Prefab);
            IPoolableFactory<Asteroid> asteroid2Factory = new PoolableFactory<Asteroid>(resolver, _asteroid2Prefab);
            IPoolableFactory<Asteroid> asteroid3Factory = new PoolableFactory<Asteroid>(resolver, _asteroid3Prefab);
                
            IPoolableFactory<LazerBullet> playerPrimaryBulletFactory = new PoolableFactory<LazerBullet>(resolver, _playerPrimaryBullet);
            IPoolableFactory<LazerBullet> playerSecondaryBulletFactory = new PoolableFactory<LazerBullet>(resolver, _playerSecondaryBullet);
            IPoolableFactory<LazerBullet> enemyBulletFactory = new PoolableFactory<LazerBullet>(resolver, _enemyBullet);
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
    }
}