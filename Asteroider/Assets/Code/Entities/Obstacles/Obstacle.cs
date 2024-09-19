using Code.Entities.Death;
using Code.Infrastructure.Pools.Poolable;
using Code.Scene.ObjectEmitting.Base;
using UnityEngine;

namespace Code.Entities.Obstacles
{
    public class Obstacle : PoolableBase<Obstacle>, IEmittable
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDeath death))
                death.Die();
            else
                Destroy(other.gameObject);
        }

        public void Emit()
        {
        }
    }
}