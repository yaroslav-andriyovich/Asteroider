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
        private PlayerMotionLimiter _motionLimiter;
        private Vector2 _direction;
        private Vector2 _smoothedDirection;
        private Vector2 _smoothedVelocity;

        public void Initialize(InputAction shipInput, PlayableZone playableZone)
        {
#if UNITY_EDITOR
            _speed /= 2f;
#endif
            _shipInput = shipInput;
            _motionLimiter = new PlayerMotionLimiter(_rigidbody, playableZone);

            InitializeInput();
        }

        public void FixedTick()
        {
            SmoothInput();
            Move();
            _motionLimiter.FixedTick();
            Rotate();
        }

        public void Dispose() => 
            _shipInput.performed -= OnMove;

        private void InitializeInput() => 
            _shipInput.performed += OnMove;

        private void OnMove(InputAction.CallbackContext ctx) => 
            _direction = ctx.ReadValue<Vector3>();
        
        private void SmoothInput() => 
            _smoothedDirection = Vector2.SmoothDamp(_smoothedDirection, _direction, ref _smoothedVelocity, SmoothTime);

        private void Move()
        {
#if UNITY_EDITOR
            _rigidbody.velocity = new Vector3(_smoothedDirection.x, 0f, _smoothedDirection.y) * _speed;
#else 
            _rigidbody.velocity = new Vector3(_direction.x, 0f, _direction.y) * _speed;
#endif
        }

        private void Rotate() => 
            _rigidbody.rotation = Quaternion.Euler(_rigidbody.velocity.z, 0f, -_rigidbody.velocity.x);
    }
}