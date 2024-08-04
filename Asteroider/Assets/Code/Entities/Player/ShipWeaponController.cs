using System;
using Code.Entities.LazerBullets;
using Code.Infrastructure;
using Code.Services.Pools;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Code.Entities.Player
{
    [Serializable]
    public class ShipWeaponController : IStartable, IDisposable
    {
        [Header("Bullet prefabs")]
        [SerializeField] private LazerBullet _primaryBulletPrefab;
        [SerializeField] private LazerBullet _secondaryBulletPrefab;
        
        [Header("Guns")]
        [SerializeField] private ShipGun _primaryGun;
        [SerializeField] private ShipGun _secondaryLeftGun;
        [SerializeField] private ShipGun _secondaryRightGun;

        [Header("Gun sounds")]
        [SerializeField] private AudioSource _primaryGunSound;
        [SerializeField] private AudioSource _secondaryGunSound;

        private PoolService _poolService;
        private InputAction _inputPrimaryGun;
        private InputAction _inputSecondaryGun;
        private ICoroutineRunner _coroutineRunner;

        public void Start()
        {
            MonoPool<LazerBullet> primaryBulletsPool = _poolService.GetPool(_primaryBulletPrefab);
            MonoPool<LazerBullet> secondaryBulletsPool = _poolService.GetPool(_secondaryBulletPrefab);
            
            _primaryGun.Initialize(primaryBulletsPool, _coroutineRunner);
            _secondaryLeftGun.Initialize(secondaryBulletsPool, _coroutineRunner);
            _secondaryRightGun.Initialize(secondaryBulletsPool, _coroutineRunner);
            
            _primaryGun.OnShot += OnPrimaryGunShot;
            _secondaryLeftGun.OnShot += OnSecondaryGunShot;
        }

        public void Dispose()
        {
            _inputPrimaryGun.started -= OnPrimaryGunActivated;
            _inputPrimaryGun.canceled -= OnPrimaryGunDeactivated;
            _inputSecondaryGun.started -= OnSecondaryGunActivated;
            _inputSecondaryGun.canceled -= OnSecondaryGunDeactivated;
            
            _primaryGun.OnShot -= OnPrimaryGunShot;
            _secondaryLeftGun.OnShot -= OnSecondaryGunShot;
        }

        public void Initialize(InputAction inputShipPrimaryShoot, InputAction inputShipSecondaryShoot, PoolService poolService, ICoroutineRunner coroutineRunner)
        {
            _poolService = poolService;
            _inputPrimaryGun = inputShipPrimaryShoot;
            _inputSecondaryGun = inputShipSecondaryShoot;
            _coroutineRunner = coroutineRunner;

            InitializeInput();
        }

        private void InitializeInput()
        {
            _inputPrimaryGun.started += OnPrimaryGunActivated;
            _inputPrimaryGun.canceled += OnPrimaryGunDeactivated;
            _inputSecondaryGun.started += OnSecondaryGunActivated;
            _inputSecondaryGun.canceled += OnSecondaryGunDeactivated;
        }

        private void OnPrimaryGunActivated(InputAction.CallbackContext ctx)
        {
            _primaryGun.Activate();
        }

        private void OnPrimaryGunDeactivated(InputAction.CallbackContext ctx)
        {
            _primaryGun.Deactivate();
        }

        private void OnSecondaryGunActivated(InputAction.CallbackContext ctx)
        {
            _secondaryLeftGun.Activate();
            _secondaryRightGun.Activate();
        }

        private void OnSecondaryGunDeactivated(InputAction.CallbackContext ctx)
        {
            _secondaryLeftGun.Deactivate();
            _secondaryRightGun.Deactivate();
        }

        private void OnPrimaryGunShot()
        {
            _primaryGunSound.Stop();
            _primaryGunSound.Play();
        }
        
        private void OnSecondaryGunShot()
        {
            _secondaryGunSound.Stop();
            _secondaryGunSound.Play();
        }
    }
}