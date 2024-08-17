using Code.ObjectEmitting;
using Code.Services.Pools;
using UnityEngine;

namespace Code.Entities.Enemy
{
    public class EnemyShip : PoolableBase<EnemyShip>, IEmittable
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private EnemyWeapon _weapon;

        public void Emit() =>
            _weapon.Activate();
    }
}