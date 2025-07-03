using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class PlayerInputRouter : MonoBehaviour
    {
        private PlayerInput _input;
        private ShipHealth _health;

        public InputAction Move => _input.Ship.Move;
        public InputAction Select => _input.Ship.Select;
        public InputAction Rocket => _input.Ship.Rocket;

        private void Awake()
        {
            _input = new PlayerInput();
            _input.Enable();
            
            _health = GetComponent<ShipHealth>();
            _health.Dying += OnDying;
        }

        private void OnDisable()
        {
            _input.Disable();
            _health.Dying -= OnDying;
        }

        private void OnDying()
        {
            _input.Disable();
        }
    }
}