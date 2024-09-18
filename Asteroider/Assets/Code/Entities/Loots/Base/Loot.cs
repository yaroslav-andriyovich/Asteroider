using Code.Utils;
using UnityEngine;

namespace Code.Entities.Loots.Base
{
    [RequireComponent(typeof(AudioSource))]
    public class Loot : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private GameObject _model;
        
        private AudioSource _audio;
        private ILootPickupBehaviour _behaviour;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _behaviour = GetComponent<ILootPickupBehaviour>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;
            
            _behaviour.PickUp(other.gameObject);
            _audio.Play();
            _collider.enabled = false;
            _model.SetActive(false);
            Destroy(gameObject, _audio.clip.length);
        }
    }
}