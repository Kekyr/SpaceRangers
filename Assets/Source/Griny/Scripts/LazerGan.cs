using UnityEngine;

namespace Enemy
{
    public class LazerGan : MonoBehaviour
    {
        [SerializeField] private GameObject _lazer;

        public void FireLazer()
        {
            _lazer.gameObject.SetActive(true);
        }

        public void DisableLazer()
        {
            _lazer.gameObject.SetActive(false);
        }
    }
}