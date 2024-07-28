using UnityEngine;

namespace Code.Entities
{
    public class Asteroid : MonoBehaviour, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    }
}