using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class KeyboardMovement : Movement
    {
        [SerializeField] private float _speed = 5f;

        private Vector3 _direction;

        protected override void Start()
        {
            base.Start();
            InputRouter.Move.canceled += OnMoveCanceled;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputRouter.Move.canceled -= OnMoveCanceled;
        }

        protected override void Move()
        {
            if (IsMoving == true)
            {
                Rigidbody.velocity = _direction * _speed;
            }
        }

        protected override void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                return;
            }

            _direction = context.ReadValue<Vector2>();
            ChangeState(true);
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                return;
            }

            Rigidbody.velocity = Vector3.zero;
            ChangeState(false);
        }
    }
}