using UnityEngine;

namespace Code.Entities
{
    public class Asteroid : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField, Min(0f)] private float _rotationSpeed;
        [SerializeField, Min(0f)] private float _minSpeed;
        [SerializeField, Min(0f)] private float _maxSpeed;
        [SerializeField] private float _angle;

        private void Awake()
        {
            _rigidbody.angularVelocity = Random.insideUnitSphere * _rotationSpeed;
            _rigidbody.velocity = new Vector3(Random.Range(-_angle, _angle), 0f, Random.Range(_minSpeed, _maxSpeed));
        }
    }
}