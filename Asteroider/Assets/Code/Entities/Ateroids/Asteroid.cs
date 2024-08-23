using Code.ObjectEmitting;
using UnityEngine;

namespace Code.Entities.Ateroids
{
    public class Asteroid : MonoBehaviour, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        public void Emit()
        {
        }
    }
}