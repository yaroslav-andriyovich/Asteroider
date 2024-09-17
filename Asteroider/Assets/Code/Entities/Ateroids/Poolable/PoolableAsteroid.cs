using Code.ObjectEmitting;
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