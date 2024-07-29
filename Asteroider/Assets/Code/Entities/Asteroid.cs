using Code.Effects;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.Entities
{
    public class Asteroid : MonoBehaviour, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        private AsteroidEffectSpawner _effectSpawner;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(GameTags.Player))
                return;

            _effectSpawner.Spawn(transform.position);
            Destroy(gameObject);
        }

        [Inject]
        public void Construct(AsteroidEffectSpawner effectSpawner) => 
            _effectSpawner = effectSpawner;
    }
}