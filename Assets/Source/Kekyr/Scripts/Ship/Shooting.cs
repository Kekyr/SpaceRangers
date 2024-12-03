using System;
using System.Collections;
using UnityEngine;

namespace Ship
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private BulletMovement _bulletPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _interval;
        [SerializeField] private float _bulletSpeed;

        private WaitForSeconds _waitForSeconds;

        private void OnEnable()
        {
            if (_bulletPrefab == null)
            {
                throw new ArgumentNullException(nameof(_bulletPrefab));
            }

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

            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                BulletMovement bullet = Instantiate(_bulletPrefab, _spawnPoint.position, _bulletPrefab.transform.rotation,
                    _spawnPoint);
                bullet.Init(_bulletSpeed);

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}