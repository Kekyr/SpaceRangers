using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CoinMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _nearSpeed;
    [SerializeField] private float _nearDistance;

    private Transform _destination;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (_jumpForce == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_jumpForce));
        }
        
        if (_nearSpeed == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_nearSpeed));
        }

        if (_nearDistance == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_nearDistance));
        }

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(_destination.position, transform.position);

        if (distance <= _nearDistance)
        {
            Vector3 direction = (_destination.position - transform.position).normalized;
            _rigidbody.velocity = direction * _nearSpeed;
        }
    }

    public void Init(Transform destination)
    {
        _destination = destination;
    }
}