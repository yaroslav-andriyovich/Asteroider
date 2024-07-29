using UnityEngine;

namespace Code.Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class ExplosionAudioEffect : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake() =>
            _audioSource = GetComponent<AudioSource>();

        public void Play()
        {
            _audioSource.Stop();
            _audioSource.Play();
        }
    }
}