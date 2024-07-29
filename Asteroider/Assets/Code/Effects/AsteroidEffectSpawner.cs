using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Effects
{
    public class AsteroidEffectSpawner : MonoBehaviour
    {
        [SerializeField] private AsteroidExplosionEffect _asteroidExplosion;
        
        private IObjectResolver _container;

        [Inject]
        public void Construct(IObjectResolver container, ObjectEmitter objectEmitter) => 
            _container = container;

        public void Spawn(Vector3 at) => 
            _container.Instantiate(_asteroidExplosion, at, Quaternion.identity);
    }
}