using System;
using System.Collections;
using Audio;
using Cinemachine;
using Game;
using UnityEngine;
using WalletSystem;

namespace ShipBase
{
    [RequireComponent(typeof(DamageHandler))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class Ship : MonoBehaviour
    {
        private readonly float _stayingDamageInterval = 1f;
        private readonly float _impulseExplosionForce = 0.08f;
        private readonly float _impulseDamageForce = 0.01f;
        private readonly Vector3 _impulseDirection = new Vector3(1, 1, 1);

        [SerializeField] private SFXSO _damageSFX;
        [SerializeField] private SFXSO _explosionSFX;
        [SerializeField] private Collider2D _shieldCollider;

        private Wallet _wallet;
        private DamageHandler _damageHandler;
        private SFX _sfx;
        private ShipHealth _health;
        private CinemachineImpulseSource _impulseSource;
        private ScreenAdjuster _screenAdjuster;
        private Collider2D _collider;
        private AudioSettingSO _sfxSetting;
        private GameEndHandler _gameEndHandler;

        private Coroutine _staying;
        private WaitForSeconds _waitInterval;

        private Vector3 _impulseExplosionVelocity;
        private Vector3 _impulseDamageVelocity;

        private void Start()
        {
            if (_shieldCollider == null)
            {
                throw new ArgumentNullException(nameof(_shieldCollider));
            }
            
            if (_damageSFX == null)
            {
                throw new ArgumentNullException(nameof(_damageSFX));
            }

            if (_explosionSFX == null)
            {
                throw new ArgumentNullException(nameof(_explosionSFX));
            }

            _health = GetComponent<ShipHealth>();
            _damageHandler = GetComponent<DamageHandler>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            _collider = GetComponent<Collider2D>();
            
            _sfx = GetComponent<SFX>();
            _sfx.Init(_sfxSetting);

            _impulseExplosionVelocity = _impulseDirection * _impulseExplosionForce;
            _impulseDamageVelocity = _impulseDirection * _impulseDamageForce;
            _waitInterval = new WaitForSeconds(_stayingDamageInterval);

            _gameEndHandler.Won += OnWon;
            _health.Dying += OnDead;
            _screenAdjuster.ResolutionChanged += OnResolutionChanged;
        }

        private void OnDestroy()
        {
            _gameEndHandler.Won -= OnWon;
            _health.Dying -= OnDead;
            _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("EnemyProjectile") || collider.gameObject.CompareTag("Enemy"))
            {
                DamageSource damageSource = collider.gameObject.GetComponent<DamageSource>();
                _damageHandler.TakeDamage(damageSource);
                _sfx.Play(_damageSFX);
                _impulseSource.GenerateImpulseWithVelocity(_impulseDamageVelocity);
            }

            if (collider.gameObject.TryGetComponent(out Coin coin))
            {
                _wallet.Add(coin.Nominal);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy") && _staying == null)
            {
                DamageSource damageSource = other.gameObject.GetComponent<DamageSource>();
                _staying = StartCoroutine(StayingIn(damageSource));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy") && _staying != null)
            {
                StopCoroutine(_staying);
                _staying = null;
            }
        }

        public void Init(Wallet wallet, ScreenAdjuster screenAdjuster, AudioSettingSO sfxSetting, GameEndHandler gameEndHandler)
        {
            _wallet = wallet;
            _screenAdjuster = screenAdjuster;
            _sfxSetting = sfxSetting;
            _gameEndHandler = gameEndHandler;
            enabled = true;
        }

        private IEnumerator StayingIn(DamageSource damageSource)
        {
            yield return _waitInterval;
            _damageHandler.TakeDamage(damageSource);
            _sfx.Play(_damageSFX);
            _staying = null;
        }

        private void OnDead()
        {
            _sfx.Play(_explosionSFX);
            _impulseSource.GenerateImpulseWithVelocity(_impulseExplosionVelocity);
        }

        private void OnResolutionChanged()
        {
            if (_collider.enabled == true)
            {
                _screenAdjuster.Clamp(transform, _collider);
            }
            else
            {
                _screenAdjuster.Clamp(transform, _shieldCollider);
            }
        }

        private void OnWon()
        {
            gameObject.SetActive(false);
        }
    }
}