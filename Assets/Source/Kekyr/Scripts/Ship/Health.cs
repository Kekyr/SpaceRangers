using System;
using UnityEngine;

namespace ShipBase
{
    public class Health : MonoBehaviour
    {
        private readonly string _damagedTrigger = "Damaged";
        private readonly string _deadTrigger = "Dead";

        [SerializeField] private Animator _animator;
        [SerializeField] private uint _max;

        private float _current;

        private float[] _partsOfHP;
        private int _currentPartOfHP;

        public event Action<float> ValueChanged;
        public event Action Died;

        public bool IsDead => _current <= 0;

        public uint Max => _max;

        private void Start()
        {
            _current = _max;

            float oneFourth = _max / 4;
            float twoFourth = oneFourth * 2;
            float threeFourth = twoFourth + oneFourth;

            _partsOfHP = new float[] { threeFourth, twoFourth, oneFourth };

            ValueChanged?.Invoke(_current);
        }

        public void TakeDamage(uint damage)
        {
            _current -= damage;

            CheckState();

            ValueChanged?.Invoke(_current);

            if (IsDead)
            {
                Died?.Invoke();
                _animator.SetTrigger(_deadTrigger);
            }
        }

        private void CheckState()
        {
            if (_currentPartOfHP >= _partsOfHP.Length)
            {
                return;
            }

            if (_current < _partsOfHP[_currentPartOfHP])
            {
                _animator.SetTrigger(_damagedTrigger);
                _currentPartOfHP++;
            }
        }
    }
}