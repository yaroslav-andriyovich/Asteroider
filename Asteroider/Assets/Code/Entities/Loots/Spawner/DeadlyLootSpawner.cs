using Code.Entities.Death;
using Code.Entities.Loots.Base;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Code.Entities.Loots.Spawner
{
    public class DeadlyLootSpawner : MonoBehaviour
    {
        [SerializeField] private RandomLootData[] _loots;
        [SerializeField, Range(0f, 1f)] private float _nothingChance;
        [SerializeField] private bool _transferVelocity = true;

        private IDeath _death;
        private IObjectResolver _objectResolver;

        private void Awake()
        {
            _death = GetComponent<IDeath>();
            _death.OnHappened += OnDeathHappened;
        }

        private void OnDestroy() =>
            _death.OnHappened -= OnDeathHappened;

        [Inject]
        public void Construct(IObjectResolver objectResolver) =>
            _objectResolver = objectResolver;

        private void OnDeathHappened() => 
            TrySpawn();

        private void TrySpawn()
        {
            float totalChance = _nothingChance;
            
            foreach (RandomLootData lootData in _loots)
            {
                totalChance += lootData.dropChance;
            }

            if (totalChance <= 0)
                return;

            float rand = Random.Range(0f, totalChance);
            float cumulativeChance = 0f;

            foreach (RandomLootData lootData in _loots)
            {
                cumulativeChance += lootData.dropChance;

                if (rand <= cumulativeChance)
                {
                    LootView lootView = _objectResolver.Instantiate(lootData.prefab, transform.position, Quaternion.identity);

                    if (_transferVelocity && TryGetComponent(out Rigidbody2D rb))
                        lootView.Rigidbody.velocity = rb.velocity;
                    
                    return;
                }
            }
        }
    }
}