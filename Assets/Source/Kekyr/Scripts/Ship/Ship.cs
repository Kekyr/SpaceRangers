using System;
using Game;
using UnityEngine;

namespace ShipBase
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private DamageHandler _damageHandler;

        private void Awake()
        {
            if (_wallet == null)
            {
                throw new ArgumentNullException(nameof(_wallet));
            }

            if (_damageHandler == null)
            {
                throw new ArgumentNullException(nameof(_damageHandler));
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                _damageHandler.TakeDamage(2);
            }

            if (col.gameObject.TryGetComponent(out Coin coin))
            {
                _wallet.Add(coin.Nominal);
            }
        }
    }
}