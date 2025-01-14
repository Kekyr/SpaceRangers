using System;
using UnityEngine;

namespace ShipBase
{
    public class ExplosionRadius : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnEnable()
        {
            if (_damage == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_damage));
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Health health = collider.gameObject.GetComponent<Health>();
                health.TakeDamage(_damage);
            }
        }
    }
}