using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    public class AutoGun : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private SFXSO _shootSFX;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _capacity;

        private Transform _parent;
        private WaitForSeconds _wait;
        private SFX _sfx;
        private EnemyHealth _health;
        private Queue<Bullet> _pool = new Queue<Bullet>();
        private bool _canShoot = true;

        public WaitForSeconds Wait => _wait;

        protected virtual void Awake()
        {
            if (_shootSFX == null)
            {
                throw new ArgumentNullException(nameof(_shootSFX));
            }

            if (_bulletPrefab == null)
            {
                throw new ArgumentNullException(nameof(_bulletPrefab));
            }

            if (_spawnPoints.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_spawnPoints));
            }

            _sfx = GetComponent<SFX>();
            _health = GetComponent<EnemyHealth>();

            _wait = new WaitForSeconds(_delay);

            _health.Died += OnDied;
        }

        protected virtual void OnEnable()
        {
            _canShoot = true;
            StartCoroutine(Prepare());
        }

        private void OnDestroy()
        {
            _health.Died -= OnDied;
        }

        private void Initialize()
        {
            GameObject container = new GameObject(_bulletPrefab.name);
            container.transform.parent = _parent;

            foreach (Transform spawnPoint in _spawnPoints)
            {
                for (int i = 0; i < _capacity; i++)
                {
                    GameObject instance = Instantiate(_bulletPrefab, spawnPoint.position, Quaternion.identity, container.transform);

                    EnemyBulletMovement enemyBulletMovement = instance.GetComponent<EnemyBulletMovement>();

                    Bullet bullet = instance.GetComponent<Bullet>();
                    bullet.gameObject.SetActive(false);
                    _pool.Enqueue(bullet);
                }
            }
        }

        public void Init(Transform parent)
        {
            _parent = parent;
            Initialize();
            enabled = true;
        }
        
        protected virtual IEnumerator Prepare()
        {
            while (_canShoot == true)
            {
                ShootBullet();
                yield return _wait;
            }
        }

        protected virtual void ShootBullet()
        {
            foreach (Transform spawnPoint in _spawnPoints)
            {
                Bullet instance = _pool.Dequeue();
                SetInstance(instance, spawnPoint);
                _pool.Enqueue(instance);
            }
        }

        private void SetInstance(Bullet bullet, Transform spawnPoint)
        {
            bullet.SetVector(spawnPoint.transform.up);
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.transform.rotation;
            bullet.gameObject.SetActive(true);
            _sfx.Play(_shootSFX);
        }

        private void OnDied()
        {
            _canShoot = false;
        }
    }
}