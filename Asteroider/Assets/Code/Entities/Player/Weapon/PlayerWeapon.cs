using Code.Services.Input;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entities.Player.Weapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Transform[] _primaryGunPoints;
        [SerializeField] private Transform[] _secondaryGunPoints;

        private IWeapon _defaultWeapon;
        private IWeapon _secondaryWeapon;
        private IWeapon _currentWeapon;

        private InputAction _inputSecondaryGun;

        private void Start() => 
            _currentWeapon.Activate();

        private void OnDestroy()
        {
            _inputSecondaryGun.started -= OnSecondaryWeaponActivated;
            _inputSecondaryGun.canceled -= OnSecondaryWeaponDeactivated;
        }

        [Inject]
        public void Construct(InputService inputService)
        {
            InputActions.PlayerActions playerActions = inputService.GetPlayerInput();

            _inputSecondaryGun = playerActions.ShipSecondaryShoot;

            InitializeInput();
        }
        
        public void Setup(IWeapon primaryWeapon, IWeapon secondaryWeapon)
        {
            _defaultWeapon = primaryWeapon;
            _secondaryWeapon = secondaryWeapon;
            _currentWeapon = primaryWeapon;

            _defaultWeapon.SetGunPoints(_primaryGunPoints);
            _secondaryWeapon.SetGunPoints(_secondaryGunPoints);
        }

        public void ChangeWeapon(IWeapon special)
        {
            _currentWeapon.Deactivate();

            Transform weaponTransform = special.gameObject.transform;

            weaponTransform.parent = transform;
            weaponTransform.localPosition = Vector3.zero;

            special.SetGunPoints(_primaryGunPoints);

            _currentWeapon = special;
            _currentWeapon.Activate();

            DOVirtual.DelayedCall(5f, () =>
            {
                _currentWeapon.Deactivate();

                if (_currentWeapon != _defaultWeapon)
                    Destroy(_currentWeapon.gameObject);

                _currentWeapon = _defaultWeapon;
                _currentWeapon.Activate();
            });
        }

        private void InitializeInput()
        {
            _inputSecondaryGun.started += OnSecondaryWeaponActivated;
            _inputSecondaryGun.canceled += OnSecondaryWeaponDeactivated;
        }

        private void OnSecondaryWeaponActivated(InputAction.CallbackContext ctx) => 
            _secondaryWeapon.Activate();

        private void OnSecondaryWeaponDeactivated(InputAction.CallbackContext ctx) => 
            _secondaryWeapon.Deactivate();
    }
}