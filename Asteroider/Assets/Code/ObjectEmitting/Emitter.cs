using System.Collections;
using Code.Utils;
using UnityEngine;
using VContainer;

namespace Code.ObjectEmitting
{
    public class Emitter<T> : MonoBehaviour where T : MonoBehaviour, IEmittable
    {
        [SerializeField] protected T[] _objectPrefabs;

        [SerializeField] protected MinMaxFloat _interval;
        [SerializeField] protected MinMaxFloat _speed;
        [SerializeField] protected MinMaxFloat _deviationAngle;
        [SerializeField] protected MinMaxFloat _rotationSpeed;

        private ObjectEmittingZone _emittingZone;

        protected virtual void Start() => 
            StartCoroutine(EmittingRoutine());

        [Inject]
        public void Construct(ObjectEmittingZone emittingZone) => 
            _emittingZone = emittingZone;

        protected IEnumerator EmittingRoutine()
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

        protected virtual T GetRandomObject() => 
            _objectPrefabs[Random.Range(0, _objectPrefabs.Length)];

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