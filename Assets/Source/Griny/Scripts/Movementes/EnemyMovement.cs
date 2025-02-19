using System;
using UnityEngine;
using WordGame;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        private const string _borderDown = "down";

        [SerializeField] private float _speed;
        
        private Rigidbody2D _rigidbody;
        private bool _isCollideDown = false;

        public event Action OutSight;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            CollideShip(collider, _speed);
            InvokeActionOutSight();
        }

        protected virtual Vector2 GetVelocity(float speed)
        {
            return Vector2.down * speed * Time.deltaTime;
        }

        protected virtual void CollideShip(Collider2D collider, float speed)
        {
            if (collider.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
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

        protected void Stop()
        {
            enabled = false;
            _rigidbody.velocity = Vector3.zero;
        }

        protected virtual void InvokeActionOutSight()
        {
            if(_isCollideDown == true)
            {
                OutSight?.Invoke();
                _isCollideDown = false;
            }
        }
    }
}