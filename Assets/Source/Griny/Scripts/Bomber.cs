using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class Bomber : EnemyShip
    {
        [SerializeField] private Health _health;
        [FormerlySerializedAs("_sheeld")] [SerializeField] private Shield shield;
        
        private void Awake()
        {
            _health.Died += OnDie;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDie;
        }

        private void OnDie()
        {
            Debug.Log("Died!");
            Deactivate();
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out ShipBase.Bullet bullet))
            {
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