using UnityEngine;

namespace Code.ObjectEmitting
{
    public interface IEmittable
    {
        Rigidbody2D Rigidbody { get; }
        void Emit();
    }
}