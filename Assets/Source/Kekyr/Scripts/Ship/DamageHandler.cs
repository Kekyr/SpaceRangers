using System;
using DG.Tweening;
using UnityEngine;

namespace ShipBase
{
    public class DamageHandler : MonoBehaviour
    {
        private readonly float _changeColorDuration = 0.15f;

        [SerializeField] private ShipHealth _health;
        [SerializeField] private Shield _shield;

        [SerializeField] private Collider2D _shieldCollider;
        [SerializeField] private Collider2D _collider;

        [SerializeField] private SpriteRenderer _shieldSpriteRenderer;

        private SpriteRenderer _spriteRenderer;
        private SpriteModifier _spriteModifier;

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

            _spriteRenderer = GetComponent<SpriteRenderer>();

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

        public void Init(SpriteModifier spriteModifier)
        {
            _spriteModifier = spriteModifier;
        }

        public void TakeDamage(Attacker attacker)
        {
            if (_shield.IsDead == false)
            {
                Sequence shieldSequence =
                    _spriteModifier.ChangeColor(_shieldSpriteRenderer, attacker.DamageColor, _changeColorDuration);
                shieldSequence.OnComplete(() => { _shield.TakeDamage(attacker.Damage); });
                return;
            }

            Sequence healthSequence =
                _spriteModifier.ChangeColor(_spriteRenderer, attacker.DamageColor, _changeColorDuration);
            healthSequence.OnComplete(() => { _health.TakeDamage(attacker.Damage); });
        }

        private void Switch(bool value)
        {
            _shieldSpriteRenderer.enabled = !value;
            _shieldCollider.enabled = !value;
            _collider.enabled = value;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_shield.IsDead == true && other.gameObject.CompareTag("Enemy"))
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