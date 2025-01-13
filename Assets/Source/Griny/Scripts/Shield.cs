using System;
using UnityEngine;

namespace Enemy
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private float _startValue;
        [SerializeField] private GameObject _sheeld;

        private float _value;

        public event Action ChangedValue;

        private void Start()
        {
            ReStartValue();
        }

        public float GetValue()
        {
            return _value;
        }

        public void ReStartValue()
        {
            _value = _startValue;
            ChangedValue?.Invoke();
            _sheeld.gameObject.SetActive(true);
        }

        public void TakeDamage(float damage)
        {
            _value -= damage;

            if (_value <= 0)
            {
                _value = 0;
                _sheeld.gameObject.SetActive(false);
            }

            ChangedValue?.Invoke();
        }
    }
}