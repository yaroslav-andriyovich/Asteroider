using Code.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace Code.Entities.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _shipMesh;
        [SerializeField] private float _speed;

        private const float SmoothTime = 0.16f;

        private InputAction _shipInput;
        private Vector2 _direction;
        private Vector2 _smoothedDirection;
        private Vector2 _smoothedVelocity;

        private void Update() => 
            Rotate();

        private void FixedUpdate()
        {
            SmoothInput();
            Move();
            //_motionLimiter.FixedTick();
        }

        private void OnDestroy() => 
            _shipInput.performed -= OnMove;

        [Inject]
        public void Construct(InputService inputService)
        {
#if UNITY_EDITOR
            _speed /= 2f;
#endif
            _shipInput = inputService.GetPlayerInput().ShipMove;
            _shipInput.performed += OnMove;
        }

        private void OnMove(InputAction.CallbackContext ctx) => 
            _direction = ctx.ReadValue<Vector3>();
        
        private void SmoothInput() => 
            _smoothedDirection = Vector2.SmoothDamp(_smoothedDirection, _direction, ref _smoothedVelocity, SmoothTime);

        private void Move()
        {
#if UNITY_EDITOR
            _rigidbody.velocity = new Vector2(_smoothedDirection.x, _smoothedDirection.y) * _speed;
#else 
            _rigidbody.velocity = new Vector2(_direction.x, _direction.y) * _speed;
#endif
        }

        private void Rotate() => 
            _shipMesh.rotation = Quaternion.Euler(_rigidbody.velocity.y - 90f, 0f, -_rigidbody.velocity.x);
    }
}