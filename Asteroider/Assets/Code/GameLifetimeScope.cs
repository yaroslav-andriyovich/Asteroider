using Code.Effects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private AsteroidEffectSpawner _asteroidEffectSpawner;
        [SerializeField] private ExplosionAudioEffect _explosionAudioEffect;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IObjectResolver, Container>(Lifetime.Scoped);
            builder.RegisterEntryPoint<Bootstrap>();
            builder.RegisterComponent(_asteroidEffectSpawner);
            builder.RegisterComponent(_explosionAudioEffect);
        }
    }
}