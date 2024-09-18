using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

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

            if (SystemInfo.supportsGyroscope)
                InputSystem.EnableDevice(Gyroscope.current);

            _playerInput.Enable();
        }

        public void Disable()
        {
            if (Accelerometer.current != null)
                InputSystem.DisableDevice(Accelerometer.current);
            
            if (Gyroscope.current != null)
                InputSystem.DisableDevice(Gyroscope.current);
            
            _playerInput.Disable();
        }

        public InputActions.PlayerActions GetPlayerInput() => 
            _playerInput;
    }
}