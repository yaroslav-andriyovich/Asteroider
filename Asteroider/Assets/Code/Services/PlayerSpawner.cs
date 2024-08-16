using Code.Entities.Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Services
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerShip _playerShipPrefab;
        
        private IObjectResolver _objectResolver;

        [Inject]
        private void Construct(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        private void Start()
        {
            _objectResolver.Instantiate(_playerShipPrefab);
        }
    }
}