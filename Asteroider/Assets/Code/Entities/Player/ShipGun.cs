using System;
using System.Collections;
using Code.Entities.LazerBullets;
using Code.Infrastructure;
using Code.Services.Pools;
using UnityEngine;

namespace Code.Entities.Player
{
    [Serializable]
    public class ShipGun
    {
        [SerializeField] private Transform _gunPoint;
        [SerializeField] private float _reloadingTime;
        
        private MonoPool<LazerBullet> _bulletsPool;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _shootingRoutine;

        public void Initialize(MonoPool<LazerBullet> bulletsPool, ICoroutineRunner coroutineRunner)
        {
            _bulletsPool = bulletsPool;
            _coroutineRunner = coroutineRunner;
        }

        public void Activate()
        {
            if (_shootingRoutine != null)
                return;
            
            _shootingRoutine = _coroutineRunner.StartCoroutine(ShootingRoutine());
        }

        public void Deactivate()
        {
            _coroutineRunner.StopCoroutine(_shootingRoutine);
            _shootingRoutine = null;
        }
        
        private IEnumerator ShootingRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_reloadingTime);

                Vector3 bulletPosition = _gunPoint.position;
                Quaternion bulletRotation = Quaternion.Euler(0f, _gunPoint.eulerAngles.y, 0f);
                LazerBullet bullet = _bulletsPool.Get(bulletPosition, bulletRotation);
                
                bullet.Rigidbody.velocity = bullet.transform.forward * bullet.Speed;
            }
        }
    }
}