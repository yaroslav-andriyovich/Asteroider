using Code.Entities.LazerBullets;
using Code.Infrastructure;
using Code.Services.Input;
using Code.Services.Pools;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerWeapon : MonoBehaviour, ICoroutineRunner
    {
        [Header("Bullet prefabs")]
        [SerializeField] private LazerBullet _primaryBulletPrefab;
        [SerializeField] private LazerBullet _secondaryBulletPrefab;
        
        [Header("Guns")]
        [SerializeField] private ShipGun _primaryGun;
        [SerializeField] private ShipGun _secondaryGun;

        [Header("Gun sounds")]
        [SerializeField] private AudioSource _primaryGunSound;
        [SerializeField] private AudioSource _secondaryGunSound;

        private PoolService _poolService;
        private InputAction _inputPrimaryGun;
        private InputAction _inputSecondaryGun;

        private void Start()
        {
            MonoPool<LazerBullet> primaryBulletsPool = _poolService.GetPool(_primaryBulletPrefab);
            MonoPool<LazerBullet> secondaryBulletsPool = _poolService.GetPool(_secondaryBulletPrefab);
            
            _primaryGun.Construct(primaryBulletsPool, this);
            _secondaryGun.Construct(secondaryBulletsPool, this);
            
            _primaryGun.OnShot += OnPrimaryGunShot;
            _secondaryGun.OnShot += OnSecondaryGunShot;
        }

        private void OnDestroy()
        {
            _inputPrimaryGun.started -= OnPrimaryGunActivated;
            _inputPrimaryGun.canceled -= OnPrimaryGunDeactivated;
            _inputSecondaryGun.started -= OnSecondaryGunActivated;
            _inputSecondaryGun.canceled -= OnSecondaryGunDeactivated;
            
            _primaryGun.OnShot -= OnPrimaryGunShot;
            _secondaryGun.OnShot -= OnSecondaryGunShot;
        }

        [Inject]
        public void Construct(InputService inputService, PoolService poolService)
        {
            InputActions.PlayerActions playerActions = inputService.GetPlayerInput();

            _inputPrimaryGun = playerActions.ShipPrimaryShoot;
            _inputSecondaryGun = playerActions.ShipSecondaryShoot;
            _poolService = poolService;

            InitializeInput();
        }

        private void InitializeInput()
        {
            _inputPrimaryGun.started += OnPrimaryGunActivated;
            _inputPrimaryGun.canceled += OnPrimaryGunDeactivated;
            _inputSecondaryGun.started += OnSecondaryGunActivated;
            _inputSecondaryGun.canceled += OnSecondaryGunDeactivated;
        }

        private void OnPrimaryGunActivated(InputAction.CallbackContext ctx) => 
            _primaryGun.Activate();

        private void OnPrimaryGunDeactivated(InputAction.CallbackContext ctx) => 
            _primaryGun.Deactivate();

        private void OnSecondaryGunActivated(InputAction.CallbackContext ctx) => 
            _secondaryGun.Activate();

        private void OnSecondaryGunDeactivated(InputAction.CallbackContext ctx) => 
            _secondaryGun.Deactivate();

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