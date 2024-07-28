using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Entities
{
    public class Asteroid : MonoBehaviour, IEmittable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField, Min(0f)] private float _rotationSpeed;
        [SerializeField, Min(0f)] private float _minSpeed;
        [SerializeField, Min(0f)] private float _maxSpeed;
        [SerializeField] private float _angle;

        void IEmittable.Emit(float forwardDirection)
        {
            _rigidbody.angularVelocity = Random.insideUnitSphere * _rotationSpeed;
            _rigidbody.velocity = forwardDirection * new Vector3(Random.Range(-_angle, _angle), 0f, Random.Range(_minSpeed, _maxSpeed));
        }
    }
}