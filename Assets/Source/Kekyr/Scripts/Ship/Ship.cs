using System;
using UnityEngine;

namespace ShipBase
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        private Health _health;

        private void Awake()
        {
            if (_collider == null)
            {
                throw new ArgumentNullException(nameof(_collider));
            }

            _health = GetComponent<Health>();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy") && _collider.enabled == true)
            {
                Debug.Log($"Collider.enabled: {_collider.enabled}");
                _health.TakeDamage(10);
            }
        }
    }
}