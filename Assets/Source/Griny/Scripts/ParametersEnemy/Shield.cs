using System;
using UnityEngine;

namespace Enemy
{
    public class Shield : MonoBehaviour, IChanging
    {
        [SerializeField] private float _startValue;
        [SerializeField] private GameObject _sheeld;

        private float _value;

        public event Action<float, float> ChangedValue;

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
            ChangedValue?.Invoke(_value, _startValue);
            _sheeld.gameObject.SetActive(true);
        }

        public void TakeDamage(float damage)
        {
            _value = Mathf.Clamp(_value - damage, 0, _startValue);

            if (_value == 0)
            {
                _sheeld.gameObject.SetActive(false);
            }

            ChangedValue?.Invoke(_value, _startValue);
        }
    }
}