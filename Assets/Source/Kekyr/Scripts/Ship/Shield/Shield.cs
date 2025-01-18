using System;
using System.Collections;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shield : MonoBehaviour
    {
        private readonly int _interval = 1;

        private ShieldDataSO _shieldData;

        private Coroutine _tryRegenerate;

        private WaitForSeconds _withoutDamage;
        private WaitForSeconds _wait;

        private float _current;

        public event Action Regenerating;
        public event Action Emptied;

        public event Action<float> ValueChanged;


        public event Action<float> Remained;

        public int Max => _shieldData.MaxHealth;
        public bool IsDead => _current <= 0;

        private void Start()
        {
            _withoutDamage = new WaitForSeconds(_shieldData.Delay);
            _wait = new WaitForSeconds(_interval);
            _current = _shieldData.MaxHealth;
            ValueChanged?.Invoke(_current);
        }

        public void Init(ShieldDataSO shieldData)
        {
            _shieldData = shieldData;
            enabled = true;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }

            _current -= damage;
            OnDamage();

            ValueChanged?.Invoke(_current);

            if (IsDead == true)
            {
                float remainingDamage = _current * -1;
                Emptied?.Invoke();
                Remained?.Invoke(remainingDamage);
                _current = 0;
            }
        }

        private IEnumerator TryRegenerate()
        {
            yield return _withoutDamage;

            Regenerating?.Invoke();

            while (_current < _shieldData.MaxHealth)
            {
                _current += _shieldData.RestoreRate;
                ValueChanged?.Invoke(_current);
                yield return _wait;
            }

            _current = _shieldData.MaxHealth;
        }

        public void OnDamage()
        {
            if (_tryRegenerate != null)
            {
                StopCoroutine(_tryRegenerate);
            }

            _tryRegenerate = StartCoroutine((TryRegenerate()));
        }

        public void OnDead()
        {
            gameObject.SetActive(false);
        }
    }
}