using UnityEngine;
using WordGame;

namespace Enemy
{
    public class Rocket : MonoBehaviour
    {
        private readonly string _downBorder = "down";
        private readonly string _destructionTrigger = "Destruct";

        private RocketMovement _rocketMovement;
        private Animator _animator;

        private void Awake()
        {
            _rocketMovement = GetComponent<RocketMovement>();
            _animator = GetComponent<Animator>();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out BackgroundBorder backgroundBorder))
            {
                if(backgroundBorder.GetName() == _downBorder)
                {
                    gameObject.SetActive(false);
                    _rocketMovement.StopRocket();
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                _rocketMovement.StopRocket();
                _animator.SetTrigger(_destructionTrigger);
                gameObject.SetActive(false);
            }
        }
    }
}