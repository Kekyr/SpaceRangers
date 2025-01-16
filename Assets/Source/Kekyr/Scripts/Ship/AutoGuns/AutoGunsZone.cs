using System;
using UnityEngine;

namespace ShipBase
{
    public class AutoGunsZone : MonoBehaviour
    {
        public event Action<GameObject> Entered;
        public event Action<GameObject> Exited;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Entered?.Invoke(collider.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Exited?.Invoke(other.gameObject);
            }
        }
    }
}
