using System;
using System.Collections;
using UnityEngine;

namespace ShipBase
{
    public class Shooting : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private ObjectPool _bulletPool;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _interval;
        [SerializeField] private float _bulletSpeed;

        private WaitForSeconds _waitForSeconds;

        private void Start()
        {
            if (_spawnPoints.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_spawnPoints));
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

            if (_spriteRenderer == null)
            {
                throw new ArgumentNullException(nameof(_spriteRenderer));
            }

            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            int orderInLayer = _spriteRenderer.sortingOrder + 1;
            
            while (true)
            {
                for (int i = 0; i < _spawnPoints.Length; i++)
                {
                    if (_spawnPoints[i].gameObject.activeSelf == true)
                    {
                        GameObject bullet = _bulletPool.Spawn(_spawnPoints[i].position);
                        BulletMovement bulletMovement = bullet.GetComponent<BulletMovement>();
                        bulletMovement.Init(_bulletSpeed, _spawnPoints[i].up, orderInLayer);
                    }
                }

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}