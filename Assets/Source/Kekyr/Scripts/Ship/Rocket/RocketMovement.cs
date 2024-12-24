using System;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Rocket))]
    public class RocketMovement : MonoBehaviour
    {
        [SerializeField] private float _force;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private Rocket _rocket;

        private void Awake()
        {
            if (_force == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_force));
            }

            _rocket = GetComponent<Rocket>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _rocket.Stopped += OnStopped;
        }

        private void OnDisable()
        {
            _rocket.Stopped -= OnStopped;
        }

        private void Start()
        {
            _animator.enabled = true;
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);
        }

        private void OnStopped()
        {
            _rigidbody.velocity = Vector2.zero;
        }
    }
}