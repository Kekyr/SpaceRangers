using UnityEngine;
using WordGame;

namespace Enemy
{
    public class Rocket : MonoBehaviour
    {
        private readonly string _player = "Player";
        private readonly string _downBorder = "down";
        private readonly string _destructionTrigger = "Destruct";

        [SerializeField] private MovementRocket _movementRocket;
        [SerializeField] private Animator _animator;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.TryGetComponent(out BackgroundBorder backgroundBorder))
            {
                if(backgroundBorder.GetName() == _downBorder)
                {
                    gameObject.SetActive(false);
                    _movementRocket.StopRocket();
                }
            }

            if (collision.gameObject.CompareTag(_player))
            {
                _movementRocket.StopRocket();
                _animator.SetTrigger(_destructionTrigger);
                gameObject.SetActive(false);
            }
        }
    }
}