using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Effects
{
    public class AsteroidExplosionEffect : PoolableBase<AsteroidExplosionEffect>
    {
        [SerializeField] private ParticleSystem _particle;

        private ExplosionAudio _explosionAudio;
        private MonoPool<AsteroidExplosionEffect> _pool;

        [Inject]
        public void Construct(ExplosionAudio explosionAudio) => 
            _explosionAudio = explosionAudio;

        public void Play()
        {
            _particle.Play();
            _explosionAudio.Play();

            Release(_particle.main.duration);
        }
    }
}