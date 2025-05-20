using System;
using Audio;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ShipBase
{
    public class RocketLauncher : MonoBehaviour
    {
        private readonly string _launchTrigger = "Launch";
        private readonly float _scalingDuration = 0.7f;
        private readonly float _newScale = 1f;
        private readonly int _addCount = 1;

        [SerializeField] private PlayerInputRouter _playerInputRouter;
        [SerializeField] private ShipHealth _health;

        [SerializeField] private GameObject[] _slots;
        [SerializeField] private GameObject _prefab;

        private Button _button;
        private Camera _camera;
        private RewardedAd _rewardedAd;
        private AudioSettingSO _sfxSetting;

        private Animator[] _slotsAnimator;
        private Rocket[] _rockets;

        private int _maxRocketCount;
        private int _rocketCount;
        private int _currentSlotIndex;
        private int _destroyedRocketCount;

        public event Action<int> CountChanged;

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
            _rewardedAd.Rewarded += OnRewarded;
            _rewardedAd.Closed += OnClosed;

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
            _rewardedAd.Rewarded -= OnRewarded;
            _rewardedAd.Closed -= OnClosed;

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
            if (_maxRocketCount != 0 && _destroyedRocketCount == _maxRocketCount && _button.interactable == false)
            {
                _currentSlotIndex = 0;
                _maxRocketCount = 0;
                _destroyedRocketCount = 0;
                _button.transform.DOScale(_newScale, _scalingDuration).SetEase(Ease.OutSine)
                    .OnComplete(() => { _button.interactable = true; });
            }
        }

        public void Init(Camera camera, Button button, int rocketCount, RewardedAd rewardedAd, AudioSettingSO sfxSetting)
        {
            _camera = camera;
            _button = button;
            _rewardedAd = rewardedAd;
            _maxRocketCount = rocketCount;
            _sfxSetting = sfxSetting;
            enabled = true;
        }

        private void Spawn(int index)
        {
            Rocket rocket = Instantiate(_prefab, _slots[index].transform).GetComponent<Rocket>();
            rocket.Init(_sfxSetting);
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
            }
        }

        private void OnRewarded()
        {
            _button.gameObject.SetActive(false);

            _maxRocketCount = _addCount;
            _rocketCount = _maxRocketCount;

            for (int i = 0; i < _maxRocketCount; i++)
            {
                Spawn(i);
            }

            CountChanged?.Invoke(_rocketCount);
        }

        private void OnClosed()
        {
            _button.gameObject.SetActive(false);
        }

        private void OnRocketDestroyed(Rocket rocket)
        {
            _destroyedRocketCount++;
        }

        private void OnDead()
        {
            gameObject.SetActive(false);
        }
    }
}