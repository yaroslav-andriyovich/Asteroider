using System;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Code.Entities.Player
{
    [Serializable]
    public class PlayerShipMovement : IFixedTickable, IDisposable
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private const float SmoothTime = 0.16f;
        
        private InputAction _shipInput;
        private Vector2 _direction;
        private Vector2 _smoothedDirection;
        private Vector2 _smoothedVelocity;

        public void Initialize(InputAction shipInput)
        {
            _shipInput = shipInput;

            InitInput();
        }

        public void FixedTick()
        {
            SmoothInput();
            Move();
            Rotate();
        }

        public void Dispose() => 
            _shipInput.performed -= OnMove;

        private void InitInput() => 
            _shipInput.performed += OnMove;

        private void OnMove(InputAction.CallbackContext ctx) => 
            _direction = ctx.ReadValue<Vector2>();

        private void SmoothInput() => 
            _smoothedDirection = Vector2.SmoothDamp(_smoothedDirection, _direction, ref _smoothedVelocity, SmoothTime);

        private void Move() => 
            _rigidbody.velocity = new Vector3(_smoothedDirection.x, 0f, _smoothedDirection.y) * _speed;

        private void Rotate() => 
            _rigidbody.rotation = Quaternion.Euler(_rigidbody.velocity.z, 0f, -_rigidbody.velocity.x);
    }
}