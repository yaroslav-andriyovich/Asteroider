using System.Collections;
using Code.Entities;
using Code.Entities.Ateroids;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.ObjectEmitting
{
    public class AsteroidEmitter : MonoBehaviour
    {
        [SerializeField] private Asteroid[] _asteroidPrefabs;

        [SerializeField] private MinMaxFloat _interval;
        [SerializeField] private MinMaxFloat _rotationSpeed;
        [SerializeField] private MinMaxFloat _speed;
        [SerializeField] private MinMaxFloat _deviationAngle;
        
        private ObjectEmittingZone _emittingZone;
        private PoolService _poolService;
        private MonoPool<Asteroid>[] _asteroidsPools;

        private void Start()
        {
            _asteroidsPools = new MonoPool<Asteroid>[_asteroidPrefabs.Length];

            for (int i = 0; i < _asteroidPrefabs.Length; i++)
                _asteroidsPools[i] = _poolService.GetPool(_asteroidPrefabs[i]);
            
            StartCoroutine(EmittingRoutine());
        }

        [Inject]
        public void Construct(ObjectEmittingZone emittingZone, PoolService poolService)
        {
            _emittingZone = emittingZone;
            _poolService = poolService;
        }
        
        private IEnumerator EmittingRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetInterval());

                Asteroid spawnedObject = GetRandomAsteroid(_emittingZone.GetRandomPosition(), Quaternion.identity);
                IEmittable component = spawnedObject.GetComponent<IEmittable>();
                
                ApplyAngularVelocity(component);
                ApplyVelocity(component);
            }
        }

        private float GetInterval() => 
            Random.Range(_interval.min, _interval.max);
        
        private Asteroid GetRandomAsteroid(Vector3 at, Quaternion rotation) => 
            _asteroidsPools[Random.Range(0, _asteroidsPools.Length)].Get(at, rotation);

        private void ApplyAngularVelocity(IEmittable component) => 
            component.Rigidbody.angularVelocity = Random.insideUnitSphere * Random.Range(_rotationSpeed.min, _rotationSpeed.max);

        private void ApplyVelocity(IEmittable component)
        {
            float deviationAngle = Random.Range(_deviationAngle.min, _deviationAngle.max);
            float speed = -Random.Range(_speed.min, _speed.max);
            
            component.Rigidbody.velocity = new Vector3(deviationAngle, 0, speed);
        }
    }
}