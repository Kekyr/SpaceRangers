using System;
using UnityEngine;

namespace Enemy
{
    public class Shield : CharacteristicEnemy
    {
        [SerializeField] private float _startValue;
        [SerializeField] private GameObject _sheeld;

        private float _value;

        private void Start()
        {
            ReStartValue();
        }

        public float GetValue()
        {
            return _value;
        }

        public float GetStartValue()
        {
            return _startValue;
        }

        public void ReStartValue()
        {
            _value = _startValue;
            GetActionChangedValue(_value, _startValue);
            _sheeld.gameObject.SetActive(true);
        }

        public void TakeDamage(float damage)
        {
            _value = Mathf.Clamp(_value - damage, 0, _startValue);

            if (_value == 0)
            {
                _sheeld.gameObject.SetActive(false);
            }

            GetActionChangedValue(_value, _startValue);
        }
    }
}