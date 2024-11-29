using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

namespace Ship
{
    [RequireComponent(typeof(PlayerInputRouter))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private readonly float _minX = -2.3f;
        private readonly float _minY = -4.7f;
        private readonly float _maxX = 2.3f;
        private readonly float _maxY = 5.7f;

        private readonly string _movingAnimation = "IsMoving";

        [SerializeField] private Animator _engineAnimator;
        [SerializeField] private float _speed;

        private PlayerInputRouter _playerInputRouter;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private PixelPerfectCamera _pixelPerfectCamera;

        private Vector3 _endPosition;

        private bool _isMoving;
        private bool _isSelected;

        private void Start()
        {
            if (_speed == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_speed));
            }

            if (_engineAnimator == null)
            {
                throw new ArgumentNullException(nameof(_engineAnimator));
            }

            _playerInputRouter = GetComponent<PlayerInputRouter>();
            _rigidbody = GetComponent<Rigidbody2D>();

            _playerInputRouter.Move.performed += OnMovePerformed;
            _playerInputRouter.Select.performed += OnSelectPerformed;
        }

        private void OnDisable()
        {
            _playerInputRouter.Move.performed -= OnMovePerformed;
            _playerInputRouter.Select.performed -= OnSelectPerformed;
        }

        private void FixedUpdate()
        {
            if (transform.position.Equals(_endPosition) == false)
            {
                _rigidbody.MovePosition(_endPosition);
            }
            else if (_isMoving == true)
            {
                _isMoving = false;
                _engineAnimator.SetBool(_movingAnimation, _isMoving);
            }
        }

        public void Init(Camera camera)
        {
            _camera = camera;
            enabled = true;
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                _isSelected = true;
            }

            if (_isSelected == true)
            {
                Vector2 mousePosition = context.ReadValue<Vector2>();
                Vector2 mouseWorldPosition = ConvertMousePosition(mousePosition);
                
                _endPosition = new Vector2(
                    Mathf.Clamp(mouseWorldPosition.x, _minX, _maxX),
                    Mathf.Clamp(mouseWorldPosition.y, _minY, _maxY));

                _isMoving = true;
                _engineAnimator.SetBool(_movingAnimation, _isMoving);
            }
        }

        private void OnSelectPerformed(InputAction.CallbackContext context)
        {
            Vector2 mouseWorldPosition = ConvertMousePosition(Mouse.current.position.ReadValue());

            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseWorldPosition, Vector2.zero);

            if (raycastHit2D.collider == null)
            {
                return;
            }

            if (raycastHit2D.collider.gameObject.TryGetComponent(out Movement movement))
            {
                _isSelected = !_isSelected;
            }
        }

        private Vector2 ConvertMousePosition(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }
    }
}