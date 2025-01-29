using System;
using ShipBase;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Shield))]
    [RequireComponent(typeof(Animator))]
    public class EnemyShip : Attacker
    {
        private readonly string _destruction = "Destruction";

        private Health _health;
        private Shield _shield;
        private Animator _animator;

        public event Action Destroyed;

        public event Action<EnemyShip> Exited;

        private void OnEnable()
        {
            _health = GetComponent<Health>();
            _shield = GetComponent<Shield>();
            _animator = GetComponent<Animator>();
            
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

        protected void Deactivate()
        {
            _animator.SetBool(_destruction, true);
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("PlayerProjectile"))
            {
                Attacker attacker = collider.gameObject.GetComponent<Attacker>();
                
                if (_shield.GetValue() <= 0)
                {
                    _health.TakeDamage(attacker.Damage);
                }
                else
                {
                    _shield.TakeDamage(attacker.Damage);
                }
            }
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
            _animator.SetBool(_destruction, false);
            gameObject.SetActive(false);
            Destroyed?.Invoke();
        }
    }
}