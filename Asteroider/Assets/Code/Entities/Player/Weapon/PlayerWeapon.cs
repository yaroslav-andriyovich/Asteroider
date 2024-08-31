using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entities.Player.Weapon
{
    public class PlayerWeapon : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Transform[] _primaryGunPoints;
        [SerializeField] private Transform[] _secondaryGunPoints;
        
        private IWeapon _defaultPrimaryWeapon;
        private IWeapon _primaryWeapon;
        private IWeapon _secondaryWeapon;
        
        private InputAction _inputPrimaryGun;
        private InputAction _inputSecondaryGun;

        private void OnDestroy()
        {
            _inputPrimaryGun.started -= OnPrimaryWeaponActivated;
            _inputPrimaryGun.canceled -= OnPrimaryWeaponDeactivated;
            _inputSecondaryGun.started -= OnSecondaryWeaponActivated;
            _inputSecondaryGun.canceled -= OnSecondaryWeaponDeactivated;
        }

        [Inject]
        public void Construct(InputService inputService)
        {
            InputActions.PlayerActions playerActions = inputService.GetPlayerInput();

            _inputPrimaryGun = playerActions.ShipPrimaryShoot;
            _inputSecondaryGun = playerActions.ShipSecondaryShoot;

            InitializeInput();
        }
        
        public void Setup(IWeapon primaryWeapon, IWeapon secondaryWeapon)
        {
            _defaultPrimaryWeapon = primaryWeapon;
            _primaryWeapon = primaryWeapon;
            _secondaryWeapon = secondaryWeapon;
            
            _primaryWeapon.SetGunPoints(_primaryGunPoints);
            _secondaryWeapon.SetGunPoints(_secondaryGunPoints);
        }

        public void ChangeWeapon(IWeapon weapon)
        {
        }

        private void InitializeInput()
        {
            _inputPrimaryGun.started += OnPrimaryWeaponActivated;
            _inputPrimaryGun.canceled += OnPrimaryWeaponDeactivated;
            
            _inputSecondaryGun.started += OnSecondaryWeaponActivated;
            _inputSecondaryGun.canceled += OnSecondaryWeaponDeactivated;
        }

        private void OnPrimaryWeaponActivated(InputAction.CallbackContext ctx) => 
            _primaryWeapon.Activate();

        private void OnPrimaryWeaponDeactivated(InputAction.CallbackContext ctx) => 
            _primaryWeapon.Deactivate();

        private void OnSecondaryWeaponActivated(InputAction.CallbackContext ctx) => 
            _secondaryWeapon.Activate();

        private void OnSecondaryWeaponDeactivated(InputAction.CallbackContext ctx) => 
            _secondaryWeapon.Deactivate();
    }
}