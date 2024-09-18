using Code.Effects;
using Code.Entities.Loots;
using Code.Scene;
using Code.Scene.ObjectEmitting;
using Code.Scene.Spawners;
using Code.Services.EndGame;
using Code.Services.Loots;
using Code.Services.OutOfBounds;
using Code.Services.Player;
using Code.Services.Pools;
using Code.Services.WorldBounds;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.DependencyInjection
{
    public class MainSceneScope : LifetimeScope
    {
        [SerializeField] private MainScenePoolInitializer _scenePoolInitializer;
        [SerializeField] private RestraintMotionService _restraintMotionService;
        [SerializeField] private ExplosionAudio _explosionAudio;
        [SerializeField] private PlayerSpawner _playerSpawner;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<WoldBoundsService>(Lifetime.Singleton);
            builder.RegisterComponent(_restraintMotionService);
            builder.Register<PoolService>(Lifetime.Singleton);
            builder.RegisterComponent(_explosionAudio);
            builder.RegisterComponentInHierarchy<ObjectEmittingZone>();
            builder.Register<PlayerProvider>(Lifetime.Singleton);
            builder.Register<LootCollectionService>(Lifetime.Singleton);
            builder.RegisterComponent(_playerSpawner);
            builder.Register<EndGameService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameOverChecker>();

            builder.RegisterBuildCallback(resolver => _scenePoolInitializer.Initialize(resolver));
        }
    }
}