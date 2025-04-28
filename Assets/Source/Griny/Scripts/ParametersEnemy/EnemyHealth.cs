using System;
using Audio;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : CharacteristicEnemy
    {
        [SerializeField] private float _startValue;

        private float _value;
        private EnemyShip _ship;

        public event Action Died;

        private void Start()
        {
            _ship = GetComponent<EnemyShip>();
            _ship.Reseted += OnReseted;
            OnReseted();
        }

        private void OnDestroy()
        {
            _ship.Reseted -= OnReseted;
        }

        public void OnReseted()
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