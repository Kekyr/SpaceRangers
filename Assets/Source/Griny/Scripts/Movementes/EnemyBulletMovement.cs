using ShipBase;
using UnityEngine;
using WordGame;

namespace Enemy
{
    [RequireComponent(typeof(Bullet))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBulletMovement : EnemyMovement
    {
        private readonly string _destructionTrigger = "Destruct";
        private readonly string _leftBorder = "left";
        private readonly string _rightBorder = "right";

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
            _collider.enabled = true;
        }
        
        protected override Vector2 GetVelocity(float speed)
        {
            return _bullet.Direction * speed * Time.deltaTime;
        }

        protected override void CollideShip(Collider2D collider, float speed)
        {
            base.CollideShip(collider, speed);

            if (collider.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
            {
                string borderName = backgruondBorder.GetName();

                if (borderName == _leftBorder || borderName == _rightBorder)
                {
                    gameObject.SetActive(false);
                }
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