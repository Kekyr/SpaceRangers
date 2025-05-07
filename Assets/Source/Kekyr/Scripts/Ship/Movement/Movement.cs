using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    [RequireComponent(typeof(PlayerInputRouter))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(ShipHealth))]
    public abstract class Movement : MonoBehaviour
    {
        private readonly string _movingAnimation = "IsMoving";

        [SerializeField] private Animator _engineAnimator;

        private PlayerInputRouter _inputRouter;
        private Rigidbody2D _rigidbody;
        private ShipHealth _health;
        private Camera _camera;
        private Canvas _canvas;

        private bool _isMoving;

        public PlayerInputRouter InputRouter => _inputRouter;

        public Rigidbody2D Rigidbody => _rigidbody;

        public bool IsMoving => _isMoving;

        public Camera MainCamera => _camera;

        public Canvas Screen => _canvas;

        protected virtual void Start()
        {
            if (_engineAnimator == null)
            {
                throw new ArgumentNullException(nameof(_engineAnimator));
            }

            _inputRouter = GetComponent<PlayerInputRouter>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<ShipHealth>();

            _health.Dying += OnDead;
            _inputRouter.Move.performed += OnMovePerformed;
        }

        protected virtual void OnDisable()
        {
            _health.Dying -= OnDead;
            _inputRouter.Move.performed -= OnMovePerformed;
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void Init(Camera mainCamera, Canvas canvas)
        {
            _camera = mainCamera;
            _canvas = canvas;
            enabled = true;
        }

        protected abstract void Move();

        protected abstract void OnMovePerformed(InputAction.CallbackContext context);

        protected void ChangeState(bool state)
        {
            _isMoving = state;
            _engineAnimator.SetBool(_movingAnimation, _isMoving);
        }

        private void OnDead()
        {
            ChangeState(false);
        }
    }
}