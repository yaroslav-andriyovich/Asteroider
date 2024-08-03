using Code.Services.Input;
using UnityEngine;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerShip : MonoBehaviour
    {
        [SerializeField] private PlayerShipMovement _movement;
        [SerializeField] private PlayerShipWeapon _weapon;

        private InputActions.PlayerActions _input;

        private void FixedUpdate()
        {
            _movement.FixedTick();
        }

        private void OnDestroy()
        {
            _movement.Dispose();
        }

        [Inject]
        public void Construct(InputService inputService)
        {
            _input = inputService.GetPlayerInput();
            
            _movement.Initialize(_input.ShipMovement);
        }
    }
}