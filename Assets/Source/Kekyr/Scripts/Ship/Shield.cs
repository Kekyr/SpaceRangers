using System;
using System.Collections;
using UnityEngine;

namespace ShipBase
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private Collider2D _healthCollider;
        [SerializeField] private Health _health;
        [SerializeField] private int _max;

        private Coroutine _tryRegenerate;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;

        private float _current;

        public event Action<float> ValueChanged;

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

            if (_max <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_max));
            }

            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();

            _current = _max;
            ValueChanged?.Invoke(_current);

            _health.ValueChanged += OnValueChanged;
        }

        private void OnDestroy()
        {
            _health.ValueChanged -= OnValueChanged;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return;
            }

            _current -= damage;

            ValueChanged?.Invoke(_current);

            if (_tryRegenerate != null)
            {
                StopCoroutine(_tryRegenerate);
            }

            _tryRegenerate = StartCoroutine(TryRegenerate());

            if (IsDead)
            {
                _current = 0;
                StartCoroutine(Switch());
            }
        }

        private IEnumerator TryRegenerate()
        {
            yield return new WaitForSeconds(5f);

            _spriteRenderer.enabled = true;
            _collider.enabled = true;
            _healthCollider.enabled = false;

            while (_current < _max)
            {
                _current += 0.1f;
                ValueChanged?.Invoke(_current);
                yield return new WaitForSeconds(0.5f);
            }

            _current = _max;
        }

        private IEnumerator Switch()
        {
            _spriteRenderer.enabled = false;
            _collider.enabled = false;

            yield return new WaitForSeconds(1f);

            _healthCollider.enabled = true;
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

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                TakeDamage(2);
            }
        }
    }
}