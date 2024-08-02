using Code.Services.Input;
using UnityEngine;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerShip : MonoBehaviour
    {
        [SerializeField] private PlayerShipMovement _movement;

        private InputActions.PlayerActions _input;

        [Inject]
        public void Construct(InputService inputService)
        {
            _input = inputService.GetPlayerInput();
            
            _movement.Initialize(_input.ShipMovement);
        }

        private void OnDestroy()
        {
            _movement.Dispose();
        }
    }
}