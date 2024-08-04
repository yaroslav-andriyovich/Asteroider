using UnityEngine;

namespace Code.Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class ExplosionAudio : MonoBehaviour
    {
        private AudioSource[] _audioSources;
        private int _lastPlayedIndex;

        private void Awake() =>
            _audioSources = GetComponents<AudioSource>();

        public void Play()
        {
            AudioSource audioSource = _audioSources[_lastPlayedIndex];
            
            audioSource.Stop();
            audioSource.Play();

            UpdateLastPlayedIndex();
        }

        private void UpdateLastPlayedIndex() => 
            _lastPlayedIndex = (int)Mathf.Repeat(_lastPlayedIndex + 1, _audioSources.Length);
    }
}