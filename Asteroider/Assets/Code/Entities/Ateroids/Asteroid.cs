using Code.Scene.ObjectEmitting;
using Code.Scene.ObjectEmitting.Base;
using UnityEngine;

namespace Code.Entities.Ateroids
{
    public class Asteroid : MonoBehaviour, IEmittable
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        public void Emit()
        {
        }
    }
}