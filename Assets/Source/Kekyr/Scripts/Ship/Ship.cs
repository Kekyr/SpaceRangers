using UnityEngine;

namespace ShipBase
{
    public class Ship : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                _health.TakeDamage(10);
            }
        }
    }
}