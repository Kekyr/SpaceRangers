using System;
using DG.Tweening;
using ShipBase;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Shield))]
    [RequireComponent(typeof(Animator))]
    public class EnemyShip : Attacker
    {
        private readonly string _destruction = "Destruction";
        private readonly float _changeColorDuration = 0.1f;

        [SerializeField] private SpriteRenderer _shieldSpriteRenderer;

        private SpriteModifier _spriteModifier;
        private SpriteRenderer _spriteRenderer;
        private Health _health;
        private Shield _shield;
        private Animator _animator;

        public event Action Destroyed;

        public event Action<EnemyShip> Exited;

        private void Awake()
        {
            if (_shieldSpriteRenderer == null)
            {
                throw new ArgumentNullException(nameof(_shieldSpriteRenderer));
            }

            _health = GetComponent<Health>();
            _shield = GetComponent<Shield>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _health.Died += OnDie;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDie;
        }

        private void OnDie()
        {
            Deactivate();
        }

        public void Init(SpriteModifier spriteModifier)
        {
            _spriteModifier = spriteModifier;
        }

        private void Deactivate()
        {
            _animator.SetBool(_destruction, true);
        }


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("PlayerProjectile"))
            {
                Attacker attacker = collider.gameObject.GetComponent<Attacker>();

                if (_shield.GetValue() <= 0)
                {
                    Sequence sequence =
                        _spriteModifier.ChangeColor(_spriteRenderer, attacker.DamageColor, _changeColorDuration);
                    sequence.OnComplete(() => { _health.TakeDamage(attacker.Damage); });
                }
                else
                {
                    Sequence sequence = _spriteModifier.ChangeColor(_shieldSpriteRenderer, attacker.DamageColor, _changeColorDuration);
                    sequence.OnComplete(() => { _shield.TakeDamage(attacker.Damage); });
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out AutoGunsZone autoGunsZone))
            {
                Exited?.Invoke(this);
            }
        }

        private void OnDestruct()
        {
            _animator.SetBool(_destruction, false);
            gameObject.SetActive(false);
            Destroyed?.Invoke();
        }
    }
}