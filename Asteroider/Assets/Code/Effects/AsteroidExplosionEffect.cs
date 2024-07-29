using DG.Tweening;
using UnityEngine;
using VContainer;

namespace Code.Effects
{
    public class AsteroidExplosionEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        
        private ExplosionAudioEffect _explosionAudioEffect;

        private void Start()
        {
            _particle.Play();
            _explosionAudioEffect.Play();

            DOVirtual.DelayedCall(_particle.main.duration, () => 
                Destroy(gameObject));
        }

        [Inject]
        public void Construct(ExplosionAudioEffect explosionAudioEffect) => 
            _explosionAudioEffect = explosionAudioEffect;
    }
}