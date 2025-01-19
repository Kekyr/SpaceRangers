using System;
using ShipBase;
using UnityEngine;

namespace Enemy
{
    public class EnemyShip : Attacker
    {
        private readonly string _destruction = "Destruction";

        [SerializeField] private Animator _animator;

        public event Action Destroyed;

        public event Action<EnemyShip> Exited;

        protected void Deactivate()
        {
            _animator.SetBool(_destruction, true);
        }

        private void OnDestruct()
        {
            _animator.SetBool(_destruction, false);
            gameObject.SetActive(false);
            Destroyed?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out AutoGunsZone autoGunsZone))
            {
                Exited?.Invoke(this);
            }
        }
    }
}