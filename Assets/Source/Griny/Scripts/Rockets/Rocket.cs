using UnityEngine;

namespace Enemy
{
    public class Rocket : Attacker
    {
        private readonly string _destructionTrigger = "Destruct";

        private RocketMovement _rocketMovement;
        private Animator _animator;

        private void Awake()
        {
            _rocketMovement = GetComponent<RocketMovement>();
            _animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderDown"))
            {
                gameObject.SetActive(false);
                _rocketMovement.Stop();
            }

            if (collider.gameObject.CompareTag("Player"))
            {
                _rocketMovement.Stop();
                _animator.SetTrigger(_destructionTrigger);
            }
        }

        private void OnDestruction()
        {
            gameObject.SetActive(false);
        }
    }
}