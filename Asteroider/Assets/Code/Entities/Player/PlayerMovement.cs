using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private const float SmoothTime = 0.16f;

        private InputAction _shipInput;
        private PlayerMotionLimiter _motionLimiter;
        private Vector2 _direction;
        private Vector2 _smoothedDirection;
        private Vector2 _smoothedVelocity;

        private void FixedUpdate()
        {
            SmoothInput();
            Move();
            _motionLimiter.FixedTick();
            Rotate();
        }

        private void OnDestroy() => 
            _shipInput.performed -= OnMove;

        [Inject]
        public void Construct(InputService inputService, PlayableZone playableZone)
        {
#if UNITY_EDITOR
            _speed /= 2f;
#endif
            _shipInput = inputService.GetPlayerInput().ShipMove;
            _shipInput.performed += OnMove;
            _motionLimiter = new PlayerMotionLimiter(_rigidbody, playableZone);
        }

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