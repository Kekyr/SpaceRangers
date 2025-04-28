using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private DirectionChanger _directionChanger;
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            if (_directionChanger == null)
            {
                throw new ArgumentNullException(nameof(_directionChanger));
            }

            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = Vector2.down;

            _directionChanger.DirectionChanged += OnDirectionChanged;
        }

        private void OnDestroy()
        {
            _directionChanger.DirectionChanged -= OnDirectionChanged;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed * Time.deltaTime;
        }

        private void OnDirectionChanged(Vector3 newDirection)
        {
            _direction = newDirection;
        }
    }
}