using Code.Entities.Player.Weapon;
using Code.ObjectEmitting;
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