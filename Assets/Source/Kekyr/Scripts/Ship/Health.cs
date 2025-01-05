using System;
using UnityEngine;

namespace ShipBase
{
    public class Health : MonoBehaviour
    {
        private readonly string _damagedTrigger = "Damaged";
        private readonly string _deadTrigger = "Dead";
        private readonly float[] _parts = new float[] { 75, 50, 25 };

        [SerializeField] private Animator _animator;
        [SerializeField] private uint _max;

        private float _current;
        private int _currentPart;

        public event Action<float> ValueChanged;
        public event Action Died;

        public bool IsDead => _current <= 0;
        public float Ratio => (_current / _max) * 100;
        public uint Max => _max;

        private void Start()
        {
            _current = _max;
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
            if (_currentPart >= _parts.Length)
            {
                return;
            }

            if (Ratio < _parts[_currentPart])
            {
                _animator.SetTrigger(_damagedTrigger);
                _currentPart++;
            }
        }
    }
}