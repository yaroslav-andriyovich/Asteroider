using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Entities.Components
{
    public class MeshRotator : MonoBehaviour
    {
        [SerializeField] private Transform _mesh;

        private Vector3 _angularVelocity;

        private void Update() => 
            _mesh.Rotate(_angularVelocity * Time.deltaTime);

        public void UpdateAngularVelocity(float speed) => 
            _angularVelocity = Random.insideUnitSphere * speed;
    }
}