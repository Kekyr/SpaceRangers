using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ship
{
    [RequireComponent(typeof(PlayerInputRouter))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private readonly string _movingAnimation = "IsMoving";

        [SerializeField] private Animator _engineAnimator;

        private PlayerInputRouter _playerInputRouter;
        private Camera _camera;
        private Rigidbody2D _rigidbody;

        private Vector2 _movePosition;

        private bool _isMoving;
        private bool _isSelected;

        private void Start()
        {
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
            if (transform.position.Equals(_movePosition) == false)
            {
                _rigidbody.MovePosition(_movePosition);
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
                _movePosition = ConvertMousePosition(context.ReadValue<Vector2>());
                _isMoving = true;
                _engineAnimator.SetBool(_movingAnimation, _isMoving);
            }
        }

        private void OnSelectPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("I'm working!");
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