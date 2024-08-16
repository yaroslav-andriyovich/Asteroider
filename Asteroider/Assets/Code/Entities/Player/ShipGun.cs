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
        [SerializeField] private Transform[] _gunPoints;
        [SerializeField] private float _reloadingTime;

        public event Action OnShot;
        
        private MonoPool<LazerBullet> _bulletsPool;
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _shootingRoutine;

        public void Construct(MonoPool<LazerBullet> bulletsPool, ICoroutineRunner coroutineRunner)
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
            if (_shootingRoutine == null)
                return;
            
            _coroutineRunner.StopCoroutine(_shootingRoutine);
            _shootingRoutine = null;
        }
        
        private IEnumerator ShootingRoutine()
        {
            while (true)
            {
                foreach (Transform point in _gunPoints)
                {
                    Vector3 bulletPosition = new Vector3(point.position.x, 0f, point.position.z);
                    Quaternion bulletRotation = Quaternion.Euler(0f, point.eulerAngles.y, 0f);
                    LazerBullet bullet = _bulletsPool.Get(bulletPosition, bulletRotation);

                    bullet.Rigidbody.velocity = bullet.transform.forward * bullet.Speed;
                    OnShot?.Invoke();
                }

                yield return new WaitForSeconds(_reloadingTime);
            }
        }
    }
}