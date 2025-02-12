using System;
using System.Collections;
using Audio;
using Cinemachine;
using Game;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Wallet))]
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

        private Wallet _wallet;
        private DamageHandler _damageHandler;
        private SFX _sfx;
        private ShipHealth _health;
        private CinemachineImpulseSource _impulseSource;

        private Vector3 _impulseExplosionVelocity;
        private Vector3 _impulseDamageVelocity;
        private Coroutine _staying;
        private WaitForSeconds _waitInterval;

        private void Awake()
        {
            if (_damageSFX == null)
            {
                throw new ArgumentNullException(nameof(_damageSFX));
            }

            if (_explosionSFX == null)
            {
                throw new ArgumentNullException(nameof(_explosionSFX));
            }

            _sfx = GetComponent<SFX>();
            _health = GetComponent<ShipHealth>();
            _wallet = GetComponent<Wallet>();
            _damageHandler = GetComponent<DamageHandler>();
            _impulseSource = GetComponent<CinemachineImpulseSource>();

            _impulseExplosionVelocity = _impulseDirection * _impulseExplosionForce;
            _impulseDamageVelocity = _impulseDirection * _impulseDamageForce;
            _waitInterval = new WaitForSeconds(_stayingDamageInterval);

            _health.Died += OnDead;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDead;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("EnemyBullet") || collider.gameObject.CompareTag("Enemy"))
            {
                Attacker attacker = collider.gameObject.GetComponent<Attacker>();
                _damageHandler.TakeDamage(attacker);
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
                Attacker attacker = other.gameObject.GetComponent<Attacker>();
                _staying = StartCoroutine(StayingIn(attacker));
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

        private IEnumerator StayingIn(Attacker attacker)
        {
            yield return _waitInterval;
            _damageHandler.TakeDamage(attacker);
            _sfx.Play(_damageSFX);
            _staying = null;
        }

        private void OnDead()
        {
            _sfx.Play(_explosionSFX);
            _impulseSource.GenerateImpulseWithVelocity(_impulseExplosionVelocity);
        }

        private void OnDestruct()
        {
            gameObject.SetActive(false);
        }
    }
}