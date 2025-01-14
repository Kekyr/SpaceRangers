using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class Scout : EnemyShip
    {
        [SerializeField] private Health _health;
        [FormerlySerializedAs("_sheeld")] [SerializeField] private Shield shield;

        private void OnEnable()
        {
            _health.Died += OnDie;
        }

        private void OnDisable()
        {
            _health.Died -= OnDie;
        }

        private void OnDie()
        { 
            Deactivate();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out ShipBase.Bullet bullet))
            {
                bullet.gameObject.SetActive(false);

                if (shield.GetValue() <= 0)
                {
                    _health.TakeDamage(bullet.Damage);
                }
                else
                {
                    shield.TakeDamage(bullet.Damage);
                }
            }
        }
    }
}