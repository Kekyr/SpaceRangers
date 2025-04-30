using System;
using UnityEngine;

namespace Enemy
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private Laser _laser;

        private void Awake()
        {
            if (_laser == null)
            {
                throw new ArgumentNullException(nameof(_laser));
            }
        }

        public void Fire()
        {
            _laser.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _laser.gameObject.SetActive(false);
        }
    }
}