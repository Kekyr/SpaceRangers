using System;
using UnityEngine;

namespace ShipBase
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private uint _start;

        private float _current;

        public event Action<float> ValueChanged;
        public event Action Died;

        public bool IsDead => _current <= 0;

        private void OnEnable()
        {
            _current = _start;
            ValueChanged?.Invoke(_current);
        }

        private void TakeDamage(uint damage)
        {
            _current -= damage;

            ValueChanged?.Invoke(_current);

            if (IsDead)
            {
                Died?.Invoke();
            }
        }
    }
}