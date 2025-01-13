using System;
using UnityEngine;
using WordGame;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private const string _borderDown = "down";

        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;

        private bool _isCollideDown = false;

        public event Action OutSight;

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CollideShip(collision, _speed);
            InvokActionOutSight();
        }

        protected virtual Vector2 GetVelocity(float speed)
        {
            return Vector2.down * speed * Time.deltaTime;
        }

        protected virtual void CollideShip(Collider2D collision, float speed)
        {
            if (collision.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
            {
                if (backgruondBorder.GetName() == _borderDown)
                {
                    speed = 0;
                    gameObject.SetActive(false);
                    _isCollideDown = true;
                }
            }
        }

        private void Move()
        {
            _rigidbody.velocity = GetVelocity(_speed);
        }

        protected virtual void InvokActionOutSight()
        {
            if(_isCollideDown == true)
            {
                OutSight?.Invoke();
                _isCollideDown = false;
            }
        }
    }
}