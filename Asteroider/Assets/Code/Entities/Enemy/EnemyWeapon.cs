using Code.Entities.LazerBullets;
using Code.Entities.Player;
using Code.Infrastructure;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Entities.Enemy
{
    public class EnemyWeapon : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LazerBullet _bulletPrefab;
        [SerializeField] private ShipGun _gun;
        
        private void OnDisable() =>
            Deactivate();

        [Inject]
        public void Construct(PoolService poolService)
        {
            MonoPool<LazerBullet> bulletsPool = poolService.GetPool(_bulletPrefab);

            _gun.Construct(bulletsPool, this);
        }

        public void Activate() => 
            _gun.Activate();
        
        public void Deactivate() => 
            _gun.Deactivate();
    }
}