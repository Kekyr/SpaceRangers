using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : CharacteristicEnemy
    {
        [SerializeField] private float _startValue;

        private float _value;
        private EnemyShip _ship;
        private bool _isDead;

        public event Action Died;

        private void Awake()
        {
            _ship = GetComponent<EnemyShip>();
            OnReseted();
        }

        private void OnEnable()
        {
            _ship.Reseted += OnReseted;
        }

        private void OnDisable()
        {
            _ship.Reseted -= OnReseted;
        }

        public void OnReseted()
        {
            _value = _startValue;
            _isDead = false;
            InvokeChangedValue(_value, _startValue);
        }

        public void TakeDamage(float damage)
        {
            _value = Mathf.Clamp(_value - damage, 0, _startValue);

            if (_value == 0 && _isDead == false)
            {
                _isDead = true;
                Died?.Invoke();
            }

            InvokeChangedValue(_value, _startValue);
        }
    }
}