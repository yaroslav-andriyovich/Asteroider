using System;
using System.Collections;
using Code.Entities.LazerBullets;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Entities.Player.Weapon
{
    [Serializable]
    public class BulletWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private LazerBullet _bulletPrefab;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private Transform[] _gunPoints;
        [SerializeField] private float _reloadingTime;

        public event Action OnFire;
        public bool IsActive => _shootingRoutine != null;

        private MonoPool<LazerBullet> _bulletsPool;
        private Coroutine _shootingRoutine;

        private void OnDisable() => 
            Deactivate();

        [Inject]
        public void Construct(PoolService poolService) => 
            _bulletsPool = poolService.GetPool(_bulletPrefab);

        public void Activate()
        {
            if (_shootingRoutine != null)
                return;

            _shootingRoutine = StartCoroutine(ShootingRoutine());
        }

        public void Deactivate()
        {
            if (_shootingRoutine == null)
                return;

            StopCoroutine(_shootingRoutine);
            _shootingRoutine = null;
        }

        public void SetGunPoints(Transform[] points) => 
            _gunPoints = points;

        private IEnumerator ShootingRoutine()
        {
            while (true)
            {
                foreach (Transform point in _gunPoints)
                {
                    Vector2 bulletPosition = new Vector2(point.position.x, point.position.y);
                    Quaternion bulletRotation = Quaternion.Euler(0f, 0f, point.eulerAngles.z);
                    LazerBullet bullet = _bulletsPool.Get(bulletPosition, bulletRotation);

                    bullet.Rigidbody.velocity = bullet.transform.up * bullet.Speed;
                    PlayAudio();
                    OnFire?.Invoke();
                }

                yield return new WaitForSeconds(_reloadingTime);
            }
        }

        private void PlayAudio()
        {
            if (_audio == null)
                return;
            
            _audio.Stop();
            _audio.Play();
        }
    }
}