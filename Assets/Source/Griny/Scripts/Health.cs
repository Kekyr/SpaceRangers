using System;
using UnityEngine;

namespace Enemy
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _startValue;

        private float _value;

        public event Action ChangedHealth;
        public event Action Died;

        private void Start()
        {
            ReStartHealth();
        }

        public float GetHealth()
        {
            return _value;
        }

        public void ReStartHealth()
        {
            _value = _startValue;
            ChangedHealth?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            _value -= damage;

            if (_value <= 0)
            {
                _value = 0;
                Died?.Invoke();
            }

            ChangedHealth?.Invoke();
        }
    }
}