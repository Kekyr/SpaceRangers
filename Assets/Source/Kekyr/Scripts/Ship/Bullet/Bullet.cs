using UnityEngine;

namespace ShipBase
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private uint _damage;

        public uint Damage => _damage;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Boundary"))
            {
                gameObject.SetActive(false);
            }

            if (collider.gameObject.CompareTag("Enemy"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}