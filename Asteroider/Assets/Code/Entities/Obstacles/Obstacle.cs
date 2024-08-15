using Code.ObjectEmitting;
using Code.Services.Pools;
using UnityEngine;

namespace Code.Entities.Obstacles
{
    public class Obstacle : PoolableBase<Obstacle>, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        public void Emit()
        {
        }
    }
}