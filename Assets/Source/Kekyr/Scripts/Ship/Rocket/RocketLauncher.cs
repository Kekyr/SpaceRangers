using System;
using UnityEngine;

namespace ShipBase
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField] private GameObject[] _slots;
        [SerializeField] private bool _canLaunch;
        [SerializeField] private int _rocketsCount;

        private Animator[] _slotsAnimator;
        private int _currentSlotIndex;

        private void Awake()
        {
            if (_slots.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_slots));
            }

            _slotsAnimator = new Animator[_slots.Length];

            for (int i = 0; i < _slots.Length; i++)
            {
                _slotsAnimator[i] = _slots[i].GetComponent<Animator>();
            }
        }

        private void FixedUpdate()
        {
            if (_canLaunch == true)
            {
                OnDoubleTap();
            }
        }

        private void OnDoubleTap()
        {
            if (_currentSlotIndex < _rocketsCount)
            {
                _slotsAnimator[_currentSlotIndex].enabled = true;
                RocketMovement rocketMovement = _slots[_currentSlotIndex].GetComponentInChildren<RocketMovement>();
                rocketMovement.enabled = true;
                _currentSlotIndex++;
            }
        }
    }
}