using System.Collections;
using Game;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(DamageHandler))]
    public class Ship : MonoBehaviour
    {
        private readonly float _stayingDamageInterval = 1f;

        private Wallet _wallet;
        private DamageHandler _damageHandler;
        private Coroutine _staying;
        private WaitForSeconds _waitInterval;

        private void Awake()
        {
            _wallet = GetComponent<Wallet>();
            _damageHandler = GetComponent<DamageHandler>();
            _waitInterval = new WaitForSeconds(_stayingDamageInterval);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("EnemyBullet") || collider.gameObject.CompareTag("Enemy"))
            {
                Attacker attacker = collider.gameObject.GetComponent<Attacker>();
                _damageHandler.TakeDamage(attacker);
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
            _staying = null;
        }

        private void OnDestruct()
        {
            gameObject.SetActive(false);
        }
    }
}