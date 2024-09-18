using Code.Entities.Player;
using Code.Entities.Weapons.Base;
using Code.Services.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Scene.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerShip _playerShipPrefab;
        [SerializeField] private GameObject _primaryWeaponPrefab;
        [SerializeField] private GameObject _secondaryWeaponPrefab;
        [SerializeField] private Vector2 _spawnPosition;
        [SerializeField] private Vector3 _spawnRotation;

        private IObjectResolver _objectResolver;
        private PlayerProvider _playerProvider;

        [Inject]
        private void Construct(IObjectResolver objectResolver, PlayerProvider playerProvider)
        {
            _playerProvider = playerProvider;
            _objectResolver = objectResolver;
        }

        private void Start()
        {
            PlayerShip ship = _objectResolver.Instantiate(_playerShipPrefab, _spawnPosition, Quaternion.Euler(_spawnRotation));
            PlayerWeapon weapon = ship.GetComponent<PlayerWeapon>();
            
            SetupWeapon(weapon, ship.transform);

            _playerProvider.Player = ship.gameObject;
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