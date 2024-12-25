using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ShipBase
{
    public class RocketLauncher : MonoBehaviour
    {
        private readonly string _launchTrigger = "Launch";

        [SerializeField] private PlayerInputRouter _playerInputRouter;

        [SerializeField] private GameObject[] _slots;
        [SerializeField] private GameObject _prefab;

        private Button _button;
        private Camera _camera;

        private Animator[] _slotsAnimator;
        private Rocket[] _rockets;

        private int _rocketCount;
        private int _currentSlotIndex;
        private int _destroyedRocketCount;

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

            _button.onClick.AddListener(OnRocketAdded);

            _slotsAnimator = new Animator[_slots.Length];
            _rockets = new Rocket[_slots.Length];

            for (int i = 0; i < _slots.Length; i++)
            {
                _slotsAnimator[i] = _slots[i].GetComponent<Animator>();

                if (i < _rocketCount)
                {
                    Spawn(i);
                }
            }
        }

        private void OnDisable()
        {
            _playerInputRouter.Rocket.performed -= OnRocketPerformed;
            _button.onClick.RemoveListener(OnRocketAdded);

            for (int i = 0; i < _rockets.Length; i++)
            {
                if (_rockets[i] != null)
                {
                    _rockets[i].Destroyed -= OnRocketDestroyed;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_destroyedRocketCount == _rocketCount && _button.interactable == false)
            {
                _currentSlotIndex = 0;
                _rocketCount = 0;
                _destroyedRocketCount = 0;
                _button.interactable = true;
            }
        }

        public void Init(Camera camera, Button button, int rocketCount)
        {
            _camera = camera;
            _button = button;
            _rocketCount = rocketCount;
            enabled = true;
        }

        private void Spawn(int index)
        {
            Rocket rocket = Instantiate(_prefab, _slots[index].transform).GetComponent<Rocket>();
            rocket.Destroyed += OnRocketDestroyed;
            _rockets[index] = rocket;
        }

        private bool CheckPointer(Vector2 position)
        {
            Vector2 pointerWorldPosition = ConvertPointerPosition(position);
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

        private void OnRocketPerformed(InputAction.CallbackContext context)
        {
            if (context.control.device is Touchscreen)
            {
                bool isSelected = CheckPointer(Pointer.current.position.value);

                if (isSelected == false)
                {
                    return;
                }
            }

            if (_rocketCount > 0 && _currentSlotIndex < _rocketCount)
            {
                Rocket rocket = _rockets[_currentSlotIndex];
                _slotsAnimator[_currentSlotIndex].SetTrigger(_launchTrigger);
                rocket.Launch();
                _currentSlotIndex++;
            }
        }

        private void OnRocketAdded()
        {
            _rocketCount = 1;

            for (int i = 0; i < _rocketCount; i++)
            {
                Spawn(i);
            }

            _button.interactable = false;
        }

        private void OnRocketDestroyed(Rocket rocket)
        {
            _destroyedRocketCount++;
        }
    }
}