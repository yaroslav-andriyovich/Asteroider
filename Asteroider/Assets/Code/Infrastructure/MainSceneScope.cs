using Code.Effects;
using Code.ObjectEmitting;
using Code.Services;
using Code.Services.Pools;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure
{
    public class MainSceneScope : LifetimeScope
    {
        [SerializeField] private MainScenePoolInitializer _scenePoolInitializer;
        [SerializeField] private PlayableZone _playableZone;
        [SerializeField] private ExplosionAudio _explosionAudio;
        [SerializeField] private PlayerSpawner _playerSpawner;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_playableZone);
            builder.Register<PoolService>(Lifetime.Singleton);
            builder.RegisterComponent(_explosionAudio);
            builder.RegisterComponentInHierarchy<ObjectEmittingZone>();
            builder.RegisterComponent(_playerSpawner);
            builder.Register<EndGameService>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameOverChecker>();

            builder.RegisterBuildCallback(resolver => _scenePoolInitializer.Initialize(resolver));
        }
    }
}