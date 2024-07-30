using Code.Pools;
using UnityEngine;
using VContainer;

namespace Code.Effects
{
    public class AsteroidExplosionEffect : PoolableBase<AsteroidExplosionEffect>
    {
        [SerializeField] private ParticleSystem _particle;

        private ExplosionAudioEffect _explosionAudioEffect;
        private MonoPool<AsteroidExplosionEffect> _pool;

        [Inject]
        public void Construct(ExplosionAudioEffect explosionAudioEffect) => 
            _explosionAudioEffect = explosionAudioEffect;

        public void Play()
        {
            _particle.Play();
            _explosionAudioEffect.Play();

            Release(_particle.main.duration);
        }
    }
}