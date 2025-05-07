using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Audio;

namespace Enemy
{
    public class RocketLauncher : MonoBehaviour
    {
        [SerializeField] private string _triggerTag;
        [SerializeField] private SFXSO _shootSFX;
        [SerializeField] private RocketMovement _prefab;
        [SerializeField] private bool _isAllAtOnce;

        private EnemyShip _ship;
        private Transform[] _spawnPoints;
        private Transform _storage;
        private List<RocketMovement> _pool = new List<RocketMovement>();
        private SFX _sfx;

        private int _currentIndex;

        private void Awake()
        {
            if (_shootSFX == null)
            {
                throw new ArgumentNullException(nameof(_shootSFX));
            }

            if (_prefab == null)
            {
                throw new ArgumentNullException(nameof(_prefab));
            }

            _ship = GetComponentInParent<EnemyShip>();
            _spawnPoints = GetComponentsInChildren<Transform>();
            _sfx = GetComponentInParent<SFX>();

            _currentIndex = 1;

            _ship.Reseted += OnReseted;
        }

        private void OnDestroy()
        {
            _ship.Reseted -= OnReseted;
        }

        private void Start()
        {
            Initialize();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag(_triggerTag))
            {
                if (_isAllAtOnce == true)
                {
                    FireAll();
                }
                else
                {
                    Fire();
                }
            }
        }

        private void Initialize()
        {
            int multiplier = 3;
            int instanceCount = _spawnPoints.Length * multiplier;

            GameObject container = new GameObject(_prefab.name);
            container.transform.parent = _storage;

            for (int i = 1; i < instanceCount; i++)
            {
                RocketMovement rocket = Instantiate(_prefab, container.transform.position, Quaternion.identity,
                    container.transform);
                rocket.gameObject.SetActive(false);
                _pool.Add(rocket);
            }
        }

        public void Init(GameObject gameObject)
        {
            _storage = gameObject.transform;
            enabled = true;
        }

        private void SetInstance(RocketMovement rocket, Transform spawnPoint)
        {
            rocket.transform.position = spawnPoint.position;
            rocket.transform.rotation = spawnPoint.rotation;
            _sfx.Play(_shootSFX);
            rocket.gameObject.SetActive(true);
            rocket.Launch();
        }

        private bool TryGetInstance(out RocketMovement result)
        {
            result = _pool.FirstOrDefault(instance => instance.gameObject.activeSelf == false);
            return result != null;
        }

        private void FireAll()
        {
            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                Launch(i);
            }
        }

        private void Fire()
        {
            if (_currentIndex == _spawnPoints.Length)
            {
                return;
            }
            
            Launch(_currentIndex);
            _currentIndex++;
        }

        private void Launch(int index)
        {
            _spawnPoints[index].gameObject.SetActive(false);

            if (TryGetInstance(out RocketMovement result))
            {
                SetInstance(result, _spawnPoints[index]);
            }
        }

        private void OnReseted()
        {
            _currentIndex = 1;
            
            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                _spawnPoints[i].gameObject.SetActive(true);
            }
        }
    }
}