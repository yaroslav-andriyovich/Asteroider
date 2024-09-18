using UnityEngine;

namespace Code.Scene.ObjectEmitting.Base
{
    public interface IEmittable
    {
        Rigidbody2D Rigidbody { get; }
        void Emit();
    }
}