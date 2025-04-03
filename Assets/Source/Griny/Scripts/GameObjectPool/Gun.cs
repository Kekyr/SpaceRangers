using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using ShipBase;
using UnityEngine;

namespace Enemy
{
    public class Gun : MonoBehaviour
    {
        private const string _paretBullets = "ParentBullets";
        private const float _delay = 0.5f;

        [SerializeField] private SFXSO _shootSFX;
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private int _capacity;

        private Transform _parent;
        private List<Bullet> _pool = new List<Bullet>();
        private WaitForSeconds _wait = new WaitForSeconds(_delay);
        private SFX _sfx;

        private Camera _camera;
        private Canvas _canvas;

        private void Awake()
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

            _sfx = GetComponentInParent<SFX>();
        }

        private void OnEnable()
        {
            StartCoroutine(ShootBullet());
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            GameObject container = new GameObject(_bulletPrefab.name);
            container.transform.parent = _parent;

            foreach (Transform spawnPoint in _spawnPoints)
            {
                for (int i = 0; i < _capacity; i++)
                {
                    GameObject instance = Instantiate(_bulletPrefab, spawnPoint.position, Quaternion.identity,
                        container.transform);

                    EnemyBulletMovement enemyBulletMovement = instance.GetComponent<EnemyBulletMovement>();
                    enemyBulletMovement.Init(_camera, _canvas);

                    Bullet bullet = instance.GetComponent<Bullet>();
                    bullet.gameObject.SetActive(false);
                    _pool.Add(bullet);
                }
            }
        }

        public void Init(Transform parent, Camera camera, Canvas canvas)
        {
            _parent = parent;
            _camera = camera;
            _canvas = canvas;
            enabled = true;
        }

        private IEnumerator ShootBullet()
        {
            while (gameObject.activeSelf == true)
            {
                foreach (Transform spawnPoint in _spawnPoints)
                {
                    if (TryGetInstance(out Bullet instance))
                    {
                        SetInstance(instance, spawnPoint);
                    }
                }

                yield return _wait;
            }
        }

        private void SetInstance(Bullet bullet, Transform spawnPoint)
        {
            bullet.SetVector(spawnPoint.transform.up);
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.transform.rotation;
            _sfx.Play(_shootSFX);
            bullet.gameObject.SetActive(true);
        }

        private bool TryGetInstance(out Bullet result)
        {
            result = _pool.FirstOrDefault(instance => instance.gameObject.activeSelf == false);
            return result != null;
        }
    }
}