using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class Movement : MonoBehaviour
    {
        private readonly string _movingAnimation = "IsMoving";

        [SerializeField] private Animator _engineAnimator;
        
        private PlayerInputRouter _playerInputRouter;
        private Rigidbody2D _rigidbody;
        private ShipHealth _health;
        
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
        }
        
        private void OnDisable()
        {
            _health.Died -= OnDead;
            _playerInputRouter.Move.performed -= OnMovePerformed;
        }
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {

        }
        
        private void OnDead()
        {
            _engineAnimator.gameObject.SetActive(false);
            enabled = false;
        }
    }
}