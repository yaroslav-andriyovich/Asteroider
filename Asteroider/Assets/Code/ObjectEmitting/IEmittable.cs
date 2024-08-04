using UnityEngine;

namespace Code.ObjectEmitting
{
    public interface IEmittable
    {
        Rigidbody Rigidbody { get; }
        void Emit();
    }
}