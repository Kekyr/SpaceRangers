using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyShip : MonoBehaviour
    {
        private readonly string _destruction = "Destruction";
        
        [SerializeField] private Animator _animator;
        public event Action Destroyed;
        
        protected IEnumerator Deactivate()
        {
            _animator.SetBool(_destruction, true);

            float animationLength = _animator.GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSeconds(animationLength);
            Debug.Log("Deactivate!");
            gameObject.SetActive(false);
            _animator.SetBool(_destruction, false);

            Destroyed?.Invoke();
        }
    }
}