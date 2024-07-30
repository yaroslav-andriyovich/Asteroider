using Code.Effects;
using Code.Pools;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ExplosionAudioEffect _explosionAudioEffect;
        [SerializeField] private AsteroidExplosionEffect _prefabExplosionEffect;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<Bootstrap>();
            builder.Register<PoolService>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponent(_explosionAudioEffect);

            builder.RegisterBuildCallback(resolver =>
            {
                PoolService poolService = resolver.Resolve<PoolService>();
                IPoolableFactory<AsteroidExplosionEffect> factory = new PoolableFactory<AsteroidExplosionEffect>(resolver, _prefabExplosionEffect);

                poolService.CreatePool(factory, 16);
            });
        }
    }
}