using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CoinMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _nearSpeed;
    [SerializeField] private float _nearDistance;

    private Transform _destination;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        if (_speed == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_speed));
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

    private void FixedUpdate()
    {
        Vector3 direction = (_destination.position - transform.position).normalized;
        float distance = Vector2.Distance(_destination.position, transform.position);
        float speed = _speed;
        
        if (distance <= _nearDistance)
        {
            speed = _nearSpeed;
        }

        _rigidbody.velocity = direction * speed;
    }

    public void Init(Transform destination)
    {
        _destination = destination;
        enabled = true;
    }
}