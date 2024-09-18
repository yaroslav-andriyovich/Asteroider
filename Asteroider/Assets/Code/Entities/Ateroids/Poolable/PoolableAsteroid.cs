using Code.Infrastructure.Pools.Poolable;
using Code.Scene.ObjectEmitting;
using Code.Scene.ObjectEmitting.Base;
using Code.Services.Pools;
using UnityEngine;

namespace Code.Entities.Ateroids.Poolable
{
    public class PoolableAsteroid : PoolableBase<PoolableAsteroid>, IEmittable
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        public void Emit()
        {
        }
    }
}