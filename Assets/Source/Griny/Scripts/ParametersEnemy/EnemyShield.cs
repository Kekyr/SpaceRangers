using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyShield : CharacteristicEnemy
    {
        [SerializeField] private float _startValue;
        [SerializeField] private GameObject _shield;

        private float _value;

        private void Start()
        {
            if (_shield == null)
            {
                throw new ArgumentNullException(nameof(_shield));
            }
            
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
            _shield.gameObject.SetActive(true);
        }

        public void TakeDamage(float damage)
        {
            _value = Mathf.Clamp(_value - damage, 0, _startValue);

            if (_value == 0)
            {
                _shield.gameObject.SetActive(false);
            }

            GetActionChangedValue(_value, _startValue);
        }
    }
}