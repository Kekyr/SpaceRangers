using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class PlayerInputRouter : MonoBehaviour
    {
        private PlayerInput _input;

        public InputAction Move => _input.Ship.Move;

        public InputAction Select => _input.Ship.Select;

        public InputAction Rocket => _input.Ship.Rocket;

        private void Awake()
        {
            _input = new PlayerInput();
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }
    }
}