using System;
using UnityEngine;

namespace ShipBase
{
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] private ShipHealth _health;
        [SerializeField] private Shield _shield;

        [SerializeField] private Collider2D _shieldCollider;
        [SerializeField] private Collider2D _collider;

        [SerializeField] private SpriteRenderer _shieldSpriteRenderer;

        private void Awake()
        {
            if (_health == null)
            {
                throw new ArgumentNullException(nameof(_health));
            }

            if (_shield == null)
            {
                throw new ArgumentNullException(nameof(_shield));
            }

            if (_shieldSpriteRenderer == null)
            {
                throw new ArgumentNullException(nameof(_shieldSpriteRenderer));
            }

            if (_shieldCollider == null)
            {
                throw new ArgumentNullException(nameof(_shieldCollider));
            }

            if (_collider == null)
            {
                throw new ArgumentNullException(nameof(_collider));
            }

            _shield.Regenerating += OnRegenerating;
            _shield.Emptied += OnEmptied;
            _shield.Remained += _health.TakeDamage;
            _health.Damaged += _shield.OnDamage;
            _health.Died += _shield.OnDead;
            _health.Died += OnDead;
        }

        private void OnDestroy()
        {
            _shield.Regenerating -= OnRegenerating;
            _shield.Emptied -= OnEmptied;
            _shield.Remained -= _health.TakeDamage;
            _health.Damaged -= _shield.OnDamage;
            _health.Died -= _shield.OnDead;
            _health.Died -= OnDead;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage));
            }

            if (_shield.IsDead == false)
            {
                _shield.TakeDamage(damage);
                return;
            }

            _health.TakeDamage(damage);
        }

        private void Switch(bool value)
        {
            _shieldSpriteRenderer.enabled = !value;
            _shieldCollider.enabled = !value;
            _collider.enabled = value;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_shield.IsDead == true)
            {
                Switch(true);
            }
        }

        private void OnRegenerating()
        {
            Switch(false);
        }

        private void OnEmptied()
        {
            _shieldSpriteRenderer.enabled = false;
        }

        private void OnDead()
        {
            _collider.enabled = false;
        }
    }
}