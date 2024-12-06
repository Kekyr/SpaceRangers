using UnityEngine;

namespace Ship
{
    public class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                return;
            }
            
            gameObject.SetActive(false);
        }
    }
}