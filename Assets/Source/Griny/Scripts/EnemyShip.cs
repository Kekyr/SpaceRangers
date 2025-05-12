using System;
using Audio;
using Cinemachine;
using DG.Tweening;
using ShipBase;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(EnemyShield))]
    [RequireComponent(typeof(SFX))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class EnemyShip : Attacker
    {
        private readonly string _destruction = "Destruction";
        private readonly float _changeColorDuration = 0.1f;
        private readonly float _impulseForce = 0.025f;
        private readonly Vector3 _impulseDirection = new Vector3(1, 1, 1);

        [SerializeField] private SpriteRenderer _shield;
        [SerializeField] private GameObject _engine;
        [SerializeField] private EnemyDataSO _data;
        [SerializeField] private SFXSO _damageSFX;
        [SerializeField] private SFXSO _explosionSFX;

        private SpriteModifier _spriteModifier;
        private ScreenAdjuster _screenAdjuster;
        private Vector3 _impulseVelocity;

        private CinemachineImpulseSource _impulseSource;
        private SpriteRenderer _spriteRenderer;
        private EnemyHealth _enemyHealth;
        private EnemyShield _enemyShield;
        private Animator _animator;
        private SFX _sfx;
        private Collider2D _collider;

        public event Action Destroyed;
        public event Action<EnemyShip> Annihilated;
        public event Action<EnemyShip> Exited;
        public event Action Reseted;

        public EnemyDataSO Data => _data;

        private void Awake()
        {
            if (_shield == null)
            {
                throw new ArgumentNullException(nameof(_shield));
            }

            if (_engine == null)
            {
                throw new ArgumentNullException(nameof(_engine));
            }

            if (_data == null)
            {
                throw new ArgumentNullException(nameof(_data));
            }

            if (_damageSFX == null)
            {
                throw new ArgumentNullException(nameof(_damageSFX));
            }

            if (_explosionSFX == null)
            {
                throw new ArgumentNullException(nameof(_explosionSFX));
            }

            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyShield = GetComponent<EnemyShield>();
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sfx = GetComponent<SFX>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            _collider = GetComponent<Collider2D>();

            _impulseVelocity = _impulseDirection * _impulseForce;
        }

        private void OnEnable()
        {
            _enemyHealth.Died += OnDie;
            _screenAdjuster.ResolutionChanged += OnResolutionChanged;
        }

        private void OnDisable()
        {
            _enemyHealth.Died -= OnDie;
            _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("PlayerProjectile"))
            {
                Attacker attacker = collider.gameObject.GetComponent<Attacker>();
                _sfx.Play(_damageSFX);

                if (_enemyShield.GetValue() <= 0)
                {
                    Sequence sequence =
                        _spriteModifier.ChangeColor(_spriteRenderer, attacker.DamageColor, _changeColorDuration);
                    sequence.OnComplete(() => { _enemyHealth.TakeDamage(attacker.Damage); });
                }
                else
                {
                    Sequence sequence = _spriteModifier.ChangeColor(_shield, attacker.DamageColor,
                        _changeColorDuration);
                    sequence.OnComplete(() => { _enemyShield.TakeDamage(attacker.Damage); });
                }
            }
        }

        public void Init(SpriteModifier spriteModifier, ScreenAdjuster screenAdjuster)
        {
            _spriteModifier = spriteModifier;
            _screenAdjuster = screenAdjuster;
            enabled = true;
        }

        public void Reset()
        {
            _engine.SetActive(true);
            Reseted?.Invoke();
        }

        private void OnDie()
        {
            Deactivate();
        }

        private void Deactivate()
        {
            _sfx.Play(_explosionSFX);
            _engine.SetActive(false);
            _animator.SetTrigger(_destruction);
            _impulseSource.GenerateImpulseWithVelocity(_impulseVelocity);
        }

        private void OnResolutionChanged()
        {
            _screenAdjuster.Clamp(transform, _collider);
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
            gameObject.SetActive(false);
            Annihilated?.Invoke(this);
            Destroyed?.Invoke();
        }
    }
}