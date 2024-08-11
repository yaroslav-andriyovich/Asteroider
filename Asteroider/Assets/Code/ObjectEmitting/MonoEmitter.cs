using System.Collections;
using Code.Services.Pools;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.ObjectEmitting
{
    public class MonoEmitter<T> : MonoBehaviour where T : MonoBehaviour, IEmittable, IPoolable<T>
    {
        [SerializeField] private T[] _objectPrefabs;

        [SerializeField] private MinMaxFloat _interval;
        [SerializeField] private MinMaxFloat _speed;
        [SerializeField] private MinMaxFloat _deviationAngle;
        [SerializeField] private MinMaxFloat _rotationSpeed;
        
        private ObjectEmittingZone _emittingZone;
        private PoolService _poolService;
        private MonoPool<T>[] _objectPools;

        private void Start()
        {
            _objectPools = new MonoPool<T>[_objectPrefabs.Length];

            for (int i = 0; i < _objectPrefabs.Length; i++)
                _objectPools[i] = _poolService.GetPool(_objectPrefabs[i]);
            
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

                T spawnedObject = GetRandomObject();
                IEmittable emittable = spawnedObject.GetComponent<IEmittable>();

                SetPosition(spawnedObject);
                ApplyVelocity(emittable);
                ApplyAngularVelocity(emittable);
                
                emittable.Emit();
            }
        }

        private float GetInterval() => 
            Random.Range(_interval.min, _interval.max);

        private T GetRandomObject() => 
            _objectPools[Random.Range(0, _objectPools.Length)].Get();

        private void SetPosition(T obj) => 
            obj.transform.position = _emittingZone.GetRandomPosition();

        private void ApplyVelocity(IEmittable component)
        {
            float deviationAngle = Random.Range(_deviationAngle.min, _deviationAngle.max);
            float speed = -Random.Range(_speed.min, _speed.max);
            
            component.Rigidbody.velocity = new Vector3(deviationAngle, 0, speed);
        }

        private void ApplyAngularVelocity(IEmittable component) => 
            component.Rigidbody.angularVelocity = Random.insideUnitSphere * Random.Range(_rotationSpeed.min, _rotationSpeed.max);
    }
}