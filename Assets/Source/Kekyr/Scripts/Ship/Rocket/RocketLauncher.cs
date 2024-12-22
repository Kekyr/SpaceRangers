using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField] private PlayerInputRouter _playerInputRouter;

        [SerializeField] private GameObject[] _slots;
        [SerializeField] private GameObject _prefab;

        [SerializeField] private int _count;

        private Camera _camera;
        private Animator[] _slotsAnimator;

        private int _currentSlotIndex;

        private void Start()
        {
            if (_playerInputRouter == null)
            {
                throw new ArgumentNullException(nameof(_playerInputRouter));
            }

            if (_slots.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_slots));
            }

            if (_prefab == null)
            {
                throw new ArgumentNullException(nameof(_prefab));
            }

            _playerInputRouter.Rocket.performed += OnRocketPerformed;

            _slotsAnimator = new Animator[_slots.Length];

            for (int i = 0; i < _slots.Length; i++)
            {
                _slotsAnimator[i] = _slots[i].GetComponent<Animator>();

                if (i < _count)
                {
                    Instantiate(_prefab, _slots[i].transform);
                }
            }
        }

        private void OnDisable()
        {
            _playerInputRouter.Rocket.performed -= OnRocketPerformed;
        }

        public void Init(Camera camera)
        {
            _camera = camera;
            enabled = true;
        }

        private void OnRocketPerformed(InputAction.CallbackContext context)
        {
            Debug.Log("Rocket Performed!");

            if (context.control.device is Touchscreen)
            {
                Debug.Log("Touchscreen");
                bool isSelected = CheckPointer(Pointer.current.position.value);

                if (isSelected == false)
                {
                    return;
                }
            }

            if (_count > 0 && _currentSlotIndex < _count)
            {
                _slotsAnimator[_currentSlotIndex].enabled = true;
                RocketMovement rocketMovement = _slots[_currentSlotIndex].GetComponentInChildren<RocketMovement>();
                rocketMovement.enabled = true;
                _currentSlotIndex++;
            }

            if (_currentSlotIndex == _count)
            {
                _currentSlotIndex = 0;
                _count = 0;
            }
        }

        private bool CheckPointer(Vector2 position)
        {
            Debug.Log($"PointerPosition: {position.x} {position.y}");
            Vector2 pointerWorldPosition = ConvertPointerPosition(position);
            Debug.Log($"PointerWorldPosition: {pointerWorldPosition.x} {pointerWorldPosition.y}");
            RaycastHit2D raycastHit = Physics2D.Raycast(pointerWorldPosition, Vector2.zero);

            if (raycastHit.collider == null)
            {
                return false;
            }

            if (raycastHit.collider.gameObject.TryGetComponent(out Movement movement))
            {
                return true;
            }

            return false;
        }

        private Vector2 ConvertPointerPosition(Vector2 mousePosition)
        {
            return _camera.ScreenToWorldPoint(mousePosition);
        }
    }
}