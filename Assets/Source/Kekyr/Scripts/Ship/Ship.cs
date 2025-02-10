using System;
using System.Collections;
using Audio;
using Enemy;
using Game;
using Unity.VisualScripting;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(DamageHandler))]
    public class Ship : MonoBehaviour
    {
        private readonly float _stayingDamageInterval = 1f;

        [SerializeField] private SFXSO _damageSFX;
        [SerializeField] private SFXSO _explosionSFX;

        private Wallet _wallet;
        private DamageHandler _damageHandler;
        private SFX _sfx;
        private ShipHealth _health;

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
        }

        private void OnDestruct()
        {
            gameObject.SetActive(false);
        }
    }
}