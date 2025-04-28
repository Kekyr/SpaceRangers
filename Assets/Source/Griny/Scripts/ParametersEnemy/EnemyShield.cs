using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyShield : CharacteristicEnemy
    {
        [SerializeField] private float _startValue;
        [SerializeField] private GameObject _shield;

        private EnemyShip _ship;
        private float _value;

        private void Start()
        {
            if (_shield == null)
            {
                throw new ArgumentNullException(nameof(_shield));
            }

            _ship = GetComponent<EnemyShip>();
            _ship.Reseted += OnReseted;
            OnReseted();
        }

        private void OnDestroy()
        {
            _ship.Reseted -= OnReseted;
        }

        public float GetValue()
        {
            return _value;
        }

        public void OnReseted()
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