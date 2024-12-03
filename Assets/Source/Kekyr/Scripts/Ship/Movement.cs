using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private PlayerInputRouter _playerInputRouter;
        private Camera _camera;
        private Rigidbody2D _rigidbody;

        private Vector3 _endPosition;

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
            _playerInputRouter.Select.started += OnSelectStarted;
            _playerInputRouter.Select.canceled += OnSelectCanceled;
        }

        private void OnDisable()
        {
            _playerInputRouter.Move.performed -= OnMovePerformed;
            _playerInputRouter.Select.started -= OnSelectStarted;
            _playerInputRouter.Select.canceled -= OnSelectCanceled;
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
            if (_isSelected == false)
            {
                return;
            }

            Vector2 pointerPosition = context.ReadValue<Vector2>();
            Vector2 pointerWorldPosition = ConvertPointerPosition(pointerPosition);

            _endPosition = new Vector2(
                Mathf.Clamp(pointerWorldPosition.x, _minX, _maxX),
                Mathf.Clamp(pointerWorldPosition.y, _minY, _maxY));

            _isMoving = true;
            _engineAnimator.SetBool(_movingAnimation, _isMoving);
        }

        private void OnSelectStarted(InputAction.CallbackContext context)
        {
            Vector2 pointerWorldPosition = ConvertPointerPosition(context.ReadValue<Vector2>());
            RaycastHit2D raycastHit = Physics2D.Raycast(pointerWorldPosition, Vector2.zero);

            if (raycastHit.collider == null)
            {
                return;
            }

            if (raycastHit.collider.gameObject.TryGetComponent(out Movement movement))
            {
                _isSelected = !_isSelected;
            }
        }

        private void OnSelectCanceled(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                _isSelected = false;
            }
        }

        private Vector2 ConvertPointerPosition(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }
    }
}