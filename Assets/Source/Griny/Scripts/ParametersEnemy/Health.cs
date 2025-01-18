using System;
using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour, IChanging
    {
        [SerializeField] private float _startValue;

        private float _value;

        public float Value => _value;
        public float StartValue => _startValue;

        public event Action Died;
        public event Action<float, float> ChangedValue;

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
            ChangedValue?.Invoke(_value, _startValue);
        }

        public void TakeDamage(float damage)
        {
            _value = Mathf.Clamp(_value - damage, 0, _startValue);

            if (_value == 0)
            {
                Died?.Invoke();
            }

            ChangedValue?.Invoke(_value, _startValue);
        }
    }
}