using Code.Infrastructure.Pools.Poolable;
using Code.Scene.ObjectEmitting;
using Code.Scene.ObjectEmitting.Base;
using Code.Services.Pools;
using UnityEngine;

namespace Code.Entities.Obstacles
{
    public class Obstacle : PoolableBase<Obstacle>, IEmittable
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        public void Emit()
        {
        }
    }
}