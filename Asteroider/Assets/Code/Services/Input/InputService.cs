using System;
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
        }

        public void Tick()
        {
        }

        public void Dispose() =>
            Disable();

        public void Enable()
        {
            if (Accelerometer.current != null)
                InputSystem.EnableDevice(Accelerometer.current);
            
            _playerInput.Enable();
        }

        public void Disable()
        {
            if (Accelerometer.current != null)
                InputSystem.DisableDevice(Accelerometer.current);
            
            _playerInput.Disable();
        }

        public InputActions.PlayerActions GetPlayerInput() => 
            _playerInput;
    }
}