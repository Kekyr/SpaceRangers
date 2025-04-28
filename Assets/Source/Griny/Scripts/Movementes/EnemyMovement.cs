using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;

        public event Action OutSight;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = Vector2.down;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed * Time.deltaTime;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderDown"))
            {
                gameObject.SetActive(false);
                OutSight?.Invoke();
            }
        }

        protected void SetNewDirection(Vector3 newDirection)
        {
            _direction = newDirection;
        }

        protected void Stop()
        {
            enabled = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}