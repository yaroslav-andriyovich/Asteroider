using Code.Infrastructure;
using Code.Services.Input;
using Code.Services.Pools;
using UnityEngine;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerShip : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private PlayerShipMovement _movement;
        [SerializeField] private ShipWeaponController _weapon;

        private InputActions.PlayerActions _input;

        private void Start()
        {
            _weapon.Start();
        }

        private void FixedUpdate()
        {
            _movement.FixedTick();
        }

        private void OnDestroy()
        {
            _movement.Dispose();
            _weapon.Dispose();
        }

        [Inject]
        public void Construct(InputService inputService, PoolService poolService)
        {
            _input = inputService.GetPlayerInput();
            
            _movement.Initialize(_input.ShipMove);
            _weapon.Initialize(_input.ShipPrimaryShoot, _input.ShipSecondaryShoot, poolService, this);
        }
    }
}