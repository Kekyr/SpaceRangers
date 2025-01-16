using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyShip : MonoBehaviour
    {
        private readonly string _destruction = "Destruction";

        [SerializeField] private Animator _animator;

        public event Action Destroyed;

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
    }
}