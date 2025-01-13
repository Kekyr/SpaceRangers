using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class Scout : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Sheeld _sheeld;
        [SerializeField] private Animator _animator;

        private float timeCurrentAnimation;
        private Coroutine _coroutine;

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
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(DiactivateEnemy());
        }

        private IEnumerator DiactivateEnemy()
        {
            _animator.SetBool("Destruction", true);

            timeCurrentAnimation = _animator.GetCurrentAnimatorStateInfo(0).length;

            yield return new WaitForSeconds(timeCurrentAnimation);

            gameObject.SetActive(false);

            _animator.SetBool("Destruction", false);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out ShipBase.Bullet bullet))
            {
                bullet.gameObject.SetActive(false);

                if (_sheeld.GetValue() <= 0)
                {
                    _health.TakeDamage(bullet.Damage);
                }
                else
                {
                    _sheeld.TakeDamage(bullet.Damage);
                }
            }
        }
    }
}