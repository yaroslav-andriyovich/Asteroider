using Code.Entities.Components.Death;
using UnityEngine;

namespace Code.Entities.Loots
{
    public class DeadlyLootSpawner : MonoBehaviour
    {
        [SerializeField] private Loot _lootPrefab;
        [SerializeField, Range(0f, 1f)] private float _chance;
        [SerializeField] private bool _transferVelocity = true;

        private IDeath _death;

        private void Awake()
        {
            _death = GetComponent<IDeath>();
            _death.OnHappened += OnDeathHappened;
        }

        private void OnDestroy() => 
            _death.OnHappened -= OnDeathHappened;

        private void OnDeathHappened()
        {
            if (!CanSpawn())
                return;
            
            Loot loot = Instantiate(_lootPrefab, transform.position, Quaternion.identity);
            
            if (_transferVelocity && TryGetComponent(out Rigidbody rb))
                loot.Rigidbody.velocity = rb.velocity;
        }

        private bool CanSpawn()
        {
            float value = Random.Range(0f, 1f);

            return value <= _chance;
        }
    }
}