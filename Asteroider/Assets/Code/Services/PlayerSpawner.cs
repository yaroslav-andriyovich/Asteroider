using Code.Entities.Player;
using Code.Entities.Player.Weapon;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Services
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerShip _playerShipPrefab;
        [SerializeField] private GameObject _primaryWeaponPrefab;
        [SerializeField] private GameObject _secondaryWeaponPrefab;
        
        private IObjectResolver _objectResolver;

        [Inject]
        private void Construct(IObjectResolver objectResolver) => 
            _objectResolver = objectResolver;

        private void Start()
        {
            PlayerShip ship = _objectResolver.Instantiate(_playerShipPrefab);
            PlayerWeapon weapon = ship.GetComponent<PlayerWeapon>();
            
            SetupWeapon(weapon, ship.transform);
        }

        private void SetupWeapon(PlayerWeapon weapon, Transform parent)
        {
            IWeapon primaryWeapon = _objectResolver.Instantiate(_primaryWeaponPrefab, parent)
                .GetComponent<IWeapon>();
            
            IWeapon secondaryWeapon = _objectResolver.Instantiate(_secondaryWeaponPrefab, parent)
                .GetComponent<IWeapon>();

            weapon.Setup(primaryWeapon, secondaryWeapon);
        }
    }
}