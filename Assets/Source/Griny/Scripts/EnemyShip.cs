using System;
using Audio;
using Cinemachine;
using DG.Tweening;
using ShipBase;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Shield))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SFX))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyShip : Attacker
    {
        private readonly string _destruction = "Destruction";
        private readonly float _changeColorDuration = 0.1f;
        private readonly float _impulseForce = 0.025f;
        private readonly Vector3 _impulseDirection = new Vector3(1, 1, 1);

        [SerializeField] private SpriteRenderer _shieldSpriteRenderer;
        [SerializeField] private SFXSO _damageSFX;
        [SerializeField] private SFXSO _explosionSFX;

        private SpriteModifier _spriteModifier;
        private Vector3 _impulseVelocity;

        private CinemachineImpulseSource _impulseSource;
        private SpriteRenderer _spriteRenderer;
        private Health _health;
        private Shield _shield;
        private Animator _animator;
        private SFX _sfx;

        public event Action Destroyed;
        public event Action<EnemyShip> Exited;

        private void Awake()
        {
            if (_impulseForce == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_impulseForce));
            }

            if (_impulseDirection == Vector3.zero)
            {
                throw new ArgumentOutOfRangeException(nameof(_impulseDirection));
            }

            if (_shieldSpriteRenderer == null)
            {
                throw new ArgumentNullException(nameof(_shieldSpriteRenderer));
            }

            if (_damageSFX == null)
            {
                throw new ArgumentNullException(nameof(_damageSFX));
            }

            if (_explosionSFX == null)
            {
                throw new ArgumentNullException(nameof(_explosionSFX));
            }

            _health = GetComponent<Health>();
            _shield = GetComponent<Shield>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sfx = GetComponent<SFX>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();

            _impulseVelocity = _impulseDirection * _impulseForce;

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
            _sfx.Play(_explosionSFX);
            _animator.SetBool(_destruction, true);
            _impulseSource.GenerateImpulseWithVelocity(_impulseVelocity);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("PlayerProjectile"))
            {
                Attacker attacker = collider.gameObject.GetComponent<Attacker>();
                _sfx.Play(_damageSFX);

                if (_shield.GetValue() <= 0)
                {
                    Sequence sequence =
                        _spriteModifier.ChangeColor(_spriteRenderer, attacker.DamageColor, _changeColorDuration);
                    sequence.OnComplete(() => { _health.TakeDamage(attacker.Damage); });
                }
                else
                {
                    Sequence sequence = _spriteModifier.ChangeColor(_shieldSpriteRenderer, attacker.DamageColor,
                        _changeColorDuration);
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