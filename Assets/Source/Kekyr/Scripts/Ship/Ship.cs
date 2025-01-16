using System;
using Game;
using UnityEngine;

namespace ShipBase
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Shield _shield;

        private ShipHealth _health;

        private void Awake()
        {
            if (_wallet == null)
            {
                throw new ArgumentNullException(nameof(_wallet));
            }

            if (_shield == null)
            {
                throw new ArgumentNullException(nameof(_shield));
            }

            _health = GetComponent<ShipHealth>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy") && _shield.IsDead == false)
            {
                _shield.TakeDamage(2);
            }
            else if (col.gameObject.CompareTag("Enemy"))
            {
                _health.TakeDamage(2);
            }

            if (col.gameObject.TryGetComponent(out Coin coin))
            {
                _wallet.Add(coin.Nominal);
            }
        }
    }
}