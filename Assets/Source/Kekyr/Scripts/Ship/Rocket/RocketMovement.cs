using System;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RocketMovement : MonoBehaviour
    {
        [SerializeField] private float _force;

        private Animator _animator;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            if (_force == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_force));
            }
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _animator.enabled = true;
            _rigidbody.AddForce(transform.up * _force, ForceMode2D.Impulse);
        }
    }
}