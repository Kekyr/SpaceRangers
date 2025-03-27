using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    [RequireComponent(typeof(PlayerInputRouter))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class TouchMovement : MonoBehaviour
    {
        private readonly string _movingAnimation = "IsMoving";

        [SerializeField] private Animator _engineAnimator;

        private PlayerInputRouter _playerInputRouter;
        private Camera _camera;
        private Rigidbody2D _rigidbody;
        private ShipHealth _health;

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
            _health = GetComponent<ShipHealth>();

            _health.Died += OnDead;
            _playerInputRouter.Move.performed += OnMovePerformed;
            _playerInputRouter.Select.started += OnSelectStarted;
            _playerInputRouter.Select.canceled += OnSelectCanceled;
        }

        private void OnDisable()
        {
            _health.Died -= OnDead;
            _playerInputRouter.Move.performed -= OnMovePerformed;
            _playerInputRouter.Select.started -= OnSelectStarted;
            _playerInputRouter.Select.canceled -= OnSelectCanceled;
        }

        private void FixedUpdate()
        {
            if (transform.position.Equals(_endPosition) == false && _isMoving == true)
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
            if (context.control.device is Keyboard)
            {
                return;
            }

            if (_isSelected == false)
            {
                return;
            }

            Vector2 pointerPosition = context.ReadValue<Vector2>();
            Vector2 pointerWorldPosition = ConvertPointerPosition(pointerPosition);

            _endPosition = pointerWorldPosition;

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

            if (raycastHit.collider.gameObject.CompareTag("Player"))
            {
                _isSelected = !_isSelected;
            }
        }

        private void OnSelectCanceled(InputAction.CallbackContext context)
        {
            _isSelected = false;
        }

        private Vector2 ConvertPointerPosition(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }

        private void OnDead()
        {
            _engineAnimator.gameObject.SetActive(false);
            enabled = false;
        }
    }
}