using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class TouchMovement : Movement
    {
        private Vector3 _endPosition;
        private bool _isSelected;

        protected override void Start()
        {
            base.Start();
            InputRouter.Select.started += OnSelectStarted;
            InputRouter.Select.canceled += OnSelectCanceled;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputRouter.Select.started -= OnSelectStarted;
            InputRouter.Select.canceled -= OnSelectCanceled;
        }

        protected override void Move()
        {
            if (transform.position.Equals(_endPosition) == false && IsMoving== true)
            {
                Rigidbody.MovePosition(_endPosition);
            }
            else if (IsMoving == true)
            {
                ChangeState(false);
            }
        }

        protected override void OnMovePerformed(InputAction.CallbackContext context)
        {
            if (context.control.device is Keyboard)
            {
                return;
            }

            if (_isSelected == false)
            {
                return;
            }

            Vector2 touchPosition = context.ReadValue<Vector2>();
            Vector2 touchWorldPosition = ConvertToWorld(touchPosition);

            _endPosition = touchWorldPosition;
            
            ChangeState(true);
            InvokePerformed();
        }

        private void OnSelectStarted(InputAction.CallbackContext context)
        {
            Vector2 touchPosition = context.ReadValue<Vector2>();
            Vector2 touchWorldPosition = ConvertToWorld(touchPosition);

            RaycastHit2D raycastHit = Physics2D.Raycast(touchWorldPosition, Vector2.zero);

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

        private Vector2 ConvertToWorld(Vector2 position)
        {
            return MainCamera.ScreenToWorldPoint(position);
        }
    }
}