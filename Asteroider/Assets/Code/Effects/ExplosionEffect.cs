using Code.Infrastructure.Pools;
using Code.Infrastructure.Pools.Poolable;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Effects
{
    public class ExplosionEffect : PoolableBase<ExplosionEffect>
    {
        [SerializeField] private ParticleSystem _particle;

        private ExplosionAudio _explosionAudio;
        private MonoPool<ExplosionEffect> _pool;

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