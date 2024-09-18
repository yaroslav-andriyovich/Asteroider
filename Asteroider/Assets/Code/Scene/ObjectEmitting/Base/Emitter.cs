using System.Collections;
using Code.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scene.ObjectEmitting.Base
{
    public class Emitter<T> : MonoBehaviour where T : MonoBehaviour, IEmittable
    {
        [SerializeField] protected T[] _objectPrefabs;

        [SerializeField] protected MinMaxFloat _interval;
        [SerializeField] protected MinMaxFloat _speed;
        [SerializeField] protected MinMaxFloat _deviationAngle;
        [SerializeField] protected MinMaxFloat _rotationSpeed;

        private ObjectEmittingZone _emittingZone;
        private IObjectResolver _objectResolver;

        protected virtual void Start() => 
            StartCoroutine(EmittingRoutine());

        [Inject]
        public void Construct(IObjectResolver objectResolver, ObjectEmittingZone emittingZone)
        {
            _objectResolver = objectResolver;
            _emittingZone = emittingZone;
        }

        protected IEnumerator EmittingRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(GetInterval());

                T spawnedObject = GetRandomObject();
                IEmittable emittable = spawnedObject.GetComponent<IEmittable>();

                SetPosition(spawnedObject);
                ApplyVelocity(emittable);
                ApplyAngularVelocity(spawnedObject.GetComponent<MeshRotator>());
                
                emittable.Emit();
            }
        }

        private float GetInterval() => 
            Random.Range(_interval.min, _interval.max);

        protected virtual T GetRandomObject() => 
            _objectResolver.Instantiate(_objectPrefabs[Random.Range(0, _objectPrefabs.Length)]);

        private void SetPosition(T obj) => 
            obj.transform.position = _emittingZone.GetRandomPosition();

        private void ApplyVelocity(IEmittable component)
        {
            float deviationAngle = Random.Range(_deviationAngle.min, _deviationAngle.max);
            float speed = -Random.Range(_speed.min, _speed.max);

            component.Rigidbody.velocity = new Vector2(deviationAngle, speed);
        }

        private void ApplyAngularVelocity(MeshRotator rotator)
        {
            if (rotator != null)
                rotator.UpdateAngularVelocity(Random.Range(_rotationSpeed.min, _rotationSpeed.max));
        }
    }
}