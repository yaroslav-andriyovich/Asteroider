using Code.Entities.Weapons.WithBullets;
using Code.Infrastructure.Pools.Poolable;
using Code.Scene.ObjectEmitting;
using Code.Scene.ObjectEmitting.Base;
using Code.Services.Pools;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyShip : PoolableBase<EnemyShip>, IEmittable
    {
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }

        [SerializeField] private BulletWeapon _weapon;

        public void Emit() => 
            _weapon.Activate();
    }
}