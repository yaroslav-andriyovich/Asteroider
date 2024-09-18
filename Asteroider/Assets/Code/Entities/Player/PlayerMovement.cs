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

        private InputActions.PlayerActions _playerInput;
        private Vector3 _inputMoveDirection;
        private Vector3 _smoothedMoveDirection;
        private Vector3 _currentSmoothedVelocity;
        private Vector3 _inputRotateDirection;
        private bool _isRotationLocked = true;

        private void FixedUpdate()
        {
            SmoothInput();
            Move();
            Rotate();
        }

        private void OnDestroy()
        {
            _playerInput.ShipMove.performed -= OnMove;
            _playerInput.ShipRotate.performed -= OnRotate;
            _playerInput.ShpRotationLock.performed -= OnToggleRotationLock;
        }

        [Inject]
        public void Construct(InputService inputService)
        {
#if UNITY_EDITOR
            _speed /= 2f;
#endif
            _playerInput = inputService.GetPlayerInput();
            
            _playerInput.ShipMove.performed += OnMove;
            _playerInput.ShipRotate.performed += OnRotate;
            _playerInput.ShpRotationLock.performed += OnToggleRotationLock;
        }

        private void OnMove(InputAction.CallbackContext ctx) => 
            _inputMoveDirection = ctx.ReadValue<Vector3>();
        
        private void OnRotate(InputAction.CallbackContext ctx) => 
            _inputRotateDirection = ctx.ReadValue<Vector3>();
        
        private void OnToggleRotationLock(InputAction.CallbackContext ctx) => 
            _isRotationLocked = !_isRotationLocked;

        private void SmoothInput() => 
            _smoothedMoveDirection = Vector3.SmoothDamp(_smoothedMoveDirection, _inputMoveDirection, ref _currentSmoothedVelocity, SmoothTime);

        private void Move()
        {
#if UNITY_EDITOR
            _rigidbody.velocity = new Vector2(_smoothedMoveDirection.x, _smoothedMoveDirection.y) * _speed;
#else 
            _rigidbody.velocity = new Vector2(_inputMoveDirection.x, _inputMoveDirection.y) * _speed;
#endif
        }

        private void Rotate()
        {
            if (_isRotationLocked)
                StabilizeShipRotation();
            else
                RotateShipObject();

            RotateShipMesh();
        }

        private void StabilizeShipRotation() => 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 1f);

        private void RotateShipObject() => 
            transform.Rotate(new Vector3(0f, 0f, _inputRotateDirection.z));

        private void RotateShipMesh()
        {
            Vector3 localEulerRotation = new Vector3(_rigidbody.velocity.y, 0f, -_rigidbody.velocity.x);
            const float correction = 90f;

            localEulerRotation.x -= correction;
            
            _shipMesh.localRotation = Quaternion.Euler(localEulerRotation);
        }
    }
}