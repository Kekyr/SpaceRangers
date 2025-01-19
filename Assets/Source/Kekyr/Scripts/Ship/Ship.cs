using System;
using Game;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Wallet))]
    [RequireComponent(typeof(DamageHandler))]
    public class Ship : MonoBehaviour
    {
        private Wallet _wallet;
        private DamageHandler _damageHandler;

        private void Awake()
        {
            _wallet = GetComponent<Wallet>();
            _damageHandler = GetComponent<DamageHandler>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Attacker attacker))
            {
                _damageHandler.TakeDamage(attacker.Damage);
            }

            if (collider.gameObject.TryGetComponent(out Coin coin))
            {
                _wallet.Add(coin.Nominal);
            }
        }

        private void OnDestruct()
        {
            gameObject.SetActive(false);
        }
    }
}