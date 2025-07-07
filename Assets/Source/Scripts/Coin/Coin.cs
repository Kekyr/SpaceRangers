using UnityEngine;

namespace Game
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _nominal;

        public int Nominal => _nominal;

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player") || 
                collider.gameObject.CompareTag("BorderDown") || 
                collider.gameObject.CompareTag("BorderUp") || 
                collider.gameObject.CompareTag("BorderRight") || 
                collider.gameObject.CompareTag("BorderLeft"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}