using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class KeyboardMovement : MonoBehaviour
    {
        private readonly string _movingAnimation = "IsMoving";
        private readonly float _speed = 5f;

        [SerializeField] private Animator _engineAnimator;

        private PlayerInputRouter _playerInputRouter;
        private Rigidbody2D _rigidbody;
        private ShipHealth _health;

        private Vector3 _direction;

        private bool _isMoving;

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
            _playerInputRouter.Move.canceled += OnMoveCanceled;
        }

        private void OnDisable()
        {
            _health.Died -= OnDead;
            _playerInputRouter.Move.performed -= OnMovePerformed;
            _playerInputRouter.Move.canceled -= OnMoveCanceled;
        }

        private void FixedUpdate()
        {
            if (_isMoving == true)
            {
                _rigidbody.velocity = _direction * _speed;
            }
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                return;
            }

            Vector3 direction = context.ReadValue<Vector2>();
            _direction = direction;
            _isMoving = true;
            _engineAnimator.SetBool(_movingAnimation, _isMoving);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                return;
            }

            _isMoving = false;
            _engineAnimator.SetBool(_movingAnimation, _isMoving);
            _rigidbody.velocity = Vector3.zero;
        }

        private void OnDead()
        {
            _engineAnimator.gameObject.SetActive(false);
            enabled = false;
        }
    }
}