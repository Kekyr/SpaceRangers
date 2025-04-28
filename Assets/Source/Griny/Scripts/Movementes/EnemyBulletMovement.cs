using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Bullet))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBulletMovement : EnemyMovement
    {
        private readonly string _destructionTrigger = "Destruct";

        private Bullet _bullet;
        private Animator _animator;
        private Collider2D _collider;

        protected override void Awake()
        {
            base.Awake();

            _bullet = GetComponent<Bullet>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            SetNewDirection(_bullet.Direction);
            _collider.enabled = true;
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderLeft") || collider.gameObject.CompareTag("BorderRight") ||
                collider.gameObject.CompareTag("BorderDown"))
            {
                gameObject.SetActive(false);
            }

            if (collider.gameObject.CompareTag("Player"))
            {
                _collider.enabled = false;
                Stop();
                _animator.SetTrigger(_destructionTrigger);
            }
        }
    }
}