using System;
using System.Collections;
using UnityEngine;

namespace Ship
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private ObjectPool _bulletPool;
        
        [SerializeField] private float _interval;
        [SerializeField] private float _bulletSpeed;

        private WaitForSeconds _waitForSeconds;

        private void OnEnable()
        {
            if (_spawnPoint == null)
            {
                throw new ArgumentNullException(nameof(_spawnPoint));
            }

            if (_interval == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_interval));
            }

            if (_bulletSpeed == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_bulletSpeed));
            }

            if (_bulletPool == null)
            {
                throw new ArgumentNullException(nameof(_bulletPool));
            }

            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                GameObject bullet = _bulletPool.Spawn(_spawnPoint.position);

                BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
                bulletMovement.Init(_bulletSpeed);

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}