using System;
using UnityEngine;

namespace Enemy
{
    public class Health : CharacteristicEnemy
    {
        [SerializeField] private float _startValue;

        private float _value;

        public event Action Died;

        public bool IsDead => _value <= 0;

        private void Start()
        {
            ResetHealth();
        }

        public float GetHealth()
        {
            return _value;
        }

        public float GetStartValue()
        {
            return _startValue;
        }

        public void ResetHealth()
        {
            _value = _startValue;
            GetActionChangedValue(_value, _startValue);
        }

        public void TakeDamage(float damage)
        {
            _value = Mathf.Clamp(_value - damage, 0, _startValue);

            if (_value == 0)
            {
                Died?.Invoke();
            }

            GetActionChangedValue(_value, _startValue);
        }
    }
}