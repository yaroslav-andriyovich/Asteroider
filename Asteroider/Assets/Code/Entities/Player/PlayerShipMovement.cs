using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Entities.Player
{
    [Serializable]
    public class PlayerShipMovement : IDisposable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;
        
        private InputAction _shipInput;
        
        public void Initialize(InputAction shipInput)
        {
            _shipInput = shipInput;

            InitInput();
        }

        public void Dispose()
        {
            _shipInput.performed -= OnMove;
        }

        private void InitInput()
        {
            _shipInput.performed += OnMove;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            
            _rigidbody.velocity = new Vector3(direction.x, 0f, direction.y) * _speed;
            _rigidbody.rotation = Quaternion.Euler(_rigidbody.velocity.z, 0f, -_rigidbody.velocity.x);
        }
    }
}