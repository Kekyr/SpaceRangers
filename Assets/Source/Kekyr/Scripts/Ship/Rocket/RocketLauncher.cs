using System;
using Audio;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShipBase
{
    public class RocketLauncher : MonoBehaviour
    {
        private readonly string _launchTrigger = "Launch";

        [SerializeField] private PlayerInputRouter _playerInputRouter;
        [SerializeField] private ShipHealth _health;

        [SerializeField] private GameObject[] _slots;
        [SerializeField] private GameObject _prefab;
        
        private Camera _camera;
        private AudioSettingSO _sfxSetting;

        private Animator[] _slotsAnimator;
        private Rocket[] _rockets;

        private int _maxRocketCount;
        private int _rocketCount;
        private int _currentSlotIndex;

        public event Action<int> CountChanged;
        public event Action Launched;

        private void Start()
        {
            if (_playerInputRouter == null)
            {
                throw new ArgumentNullException(nameof(_playerInputRouter));
            }

            if (_health == null)
            {
                throw new ArgumentNullException(nameof(_health));
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
            _health.Dying += OnDead;

            _rocketCount = _maxRocketCount;
            CountChanged?.Invoke(_rocketCount);

            _slotsAnimator = new Animator[_slots.Length];
            _rockets = new Rocket[_slots.Length];

            for (int i = 0; i < _slots.Length; i++)
            {
                _slotsAnimator[i] = _slots[i].GetComponent<Animator>();

                if (i < _maxRocketCount)
                {
                    Spawn(i);
                }
            }
        }

        private void OnDisable()
        {
            _playerInputRouter.Rocket.performed -= OnRocketPerformed;
            _health.Dying -= OnDead;
        }

        public void Init(Camera camera, int rocketCount, AudioSettingSO sfxSetting)
        {
            _camera = camera;
            _maxRocketCount = rocketCount;
            _sfxSetting = sfxSetting;
            enabled = true;
        }

        private void Spawn(int index)
        {
            Rocket rocket = Instantiate(_prefab, _slots[index].transform).GetComponent<Rocket>();
            rocket.Init(_sfxSetting);
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

            if (raycastHit.collider.gameObject.CompareTag("Player"))
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

            if (_maxRocketCount > 0 && _currentSlotIndex < _maxRocketCount)
            {
                Rocket rocket = _rockets[_currentSlotIndex];
                _slotsAnimator[_currentSlotIndex].SetTrigger(_launchTrigger);
                rocket.Launch();
                _rocketCount--;
                CountChanged?.Invoke(_rocketCount);
                _currentSlotIndex++;
                Launched?.Invoke();
            }
        }

        private void OnDead()
        {
            gameObject.SetActive(false);
        }
    }
}