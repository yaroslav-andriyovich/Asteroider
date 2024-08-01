using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Code.Services.Input
{
    public class InputService : ITickable, IDisposable
    {
        private InputActions.PlayerActions _playerInput;

        public InputService(InputActions inputActions)
        {
            _playerInput = inputActions.Player;
            
            Enable();
            _playerInput.ShipMovement.performed += ShipMovementOnperformed;
        }

        private void ShipMovementOnperformed(InputAction.CallbackContext ctx)
        {
            Debug.Log(ctx.ReadValue<Vector2>());
        }

        public void Tick()
        {
            //IsTap = _isTouch || _playerInput.TapSimulationByKeyboard.IsPressed();
        }

        public void Dispose() =>
            Disable();

        public void Enable()
        {
            _playerInput.Enable();
        }

        public void Disable()
        {
            _playerInput.Disable();
        }

        public InputActions.PlayerActions GetPlayerInput() => 
            _playerInput;
    }
}