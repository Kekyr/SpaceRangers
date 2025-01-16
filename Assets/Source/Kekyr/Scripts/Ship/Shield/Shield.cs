using System;
using System.Collections;
using UnityEngine;

namespace ShipBase
{
    public class Shield : MonoBehaviour
    {
        private readonly int _interval = 1;

        [SerializeField] private Collider2D _healthCollider;
        [SerializeField] private ShipHealth _health;

        private Coroutine _tryRegenerate;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private WaitForSeconds _withoutDamage;
        private WaitForSeconds _wait;

        private int _max;
        private int _delay;

        private float _current;
        private float _speed;

        public event Action<float> ValueChanged;
        public event Action<Collider2D> Entered;

        public int Max => _max;
        public bool IsDead => _current <= 0;

        private void Start()
        {
            if (_healthCollider == null)
            {
                throw new ArgumentNullException(nameof(_healthCollider));
            }

            if (_health == null)
            {
                throw new ArgumentNullException(nameof(_health));
            }

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            _withoutDamage = new WaitForSeconds(_delay);
            _wait = new WaitForSeconds(_interval);

            _current = _max;
            ValueChanged?.Invoke(_current);

            _health.ValueChanged += OnValueChanged;
            _health.Died += OnDead;
        }

        private void OnDestroy()
        {
            _health.ValueChanged -= OnValueChanged;
            _health.Died -= OnDead;
        }

        public void Init(ShieldDataSO shieldData)
        {
            _max = shieldData.MaxHealth;
            _delay = shieldData.Delay;
            _speed = shieldData.Speed;
            enabled = true;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }

            _current -= damage;

            ValueChanged?.Invoke(_current);

            if (_tryRegenerate != null)
            {
                StopCoroutine(_tryRegenerate);
            }

            _tryRegenerate = StartCoroutine(TryRegenerate());

            if (IsDead == true)
            {
                _spriteRenderer.enabled = false;

                if (_current < 0)
                {
                    float remainingDamage = _current * -1;
                    _health.TakeDamage(remainingDamage);
                }

                _current = 0;
            }
        }

        private IEnumerator TryRegenerate()
        {
            yield return _withoutDamage;

            _spriteRenderer.enabled = true;
            Switch(false);

            while (_current < _max)
            {
                _current += _speed;
                ValueChanged?.Invoke(_current);
                yield return _wait;
            }

            _current = _max;
        }

        private void Switch(bool value)
        {
            _collider.enabled = !value;
            _healthCollider.enabled = value;
        }

        private void OnValueChanged(float value)
        {
            if (IsDead == false)
            {
                return;
            }

            StopCoroutine(_tryRegenerate);
            _tryRegenerate = StartCoroutine((TryRegenerate()));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (IsDead == true)
            {
                Switch(true);
            }
        }

        private void OnDead()
        {
            gameObject.SetActive(false);
        }
    }
}