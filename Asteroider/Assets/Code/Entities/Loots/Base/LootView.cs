using Code.Services.Loots;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities.Loots.Base
{
    [RequireComponent(typeof(AudioSource))]
    public class LootView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private GameObject _model;
        
        private AudioSource _audio;
        private Loot _loot;
        private LootCollectionService _lootCollectionService;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
            _loot = GetComponent<Loot>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;
            
            _lootCollectionService.OnCollect(_loot);
            _audio.Play();
            _collider.enabled = false;
            _model.SetActive(false);
            Destroy(gameObject, _audio.clip.length);
        }

        [Inject]
        public void Construct(LootCollectionService lootCollectionService) => 
            _lootCollectionService = lootCollectionService;
    }
}