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

        private EnemyShip _ship;
        private Transform[] _spawnPoints;
        private Transform _parent;
        private List<RocketMovement> _pool = new List<RocketMovement>();
        private SFX _sfx;

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
                Fire();
            }
        }

        private void Initialize()
        {
            GameObject container = new GameObject(_prefab.name);
            container.transform.parent = _parent;

            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                RocketMovement rocket = Instantiate(_prefab, _spawnPoints[i].position, Quaternion.identity,
                    container.transform);
                rocket.gameObject.SetActive(false);
                _pool.Add(rocket);
            }
        }

        public void Init(GameObject gameObject)
        {
            _parent = gameObject.transform;
            enabled = true;
        }

        private void SetInstance(RocketMovement rocket, Transform spawnPoint)
        {
            rocket.transform.position = spawnPoint.position;
            rocket.transform.rotation = spawnPoint.rotation;
            _sfx.Play(_shootSFX);
            rocket.gameObject.SetActive(true);
            rocket.RunRocket();
        }

        private bool TryGetInstance(out RocketMovement result)
        {
            result = _pool.FirstOrDefault(instance => instance.gameObject.activeSelf == false);
            return result != null;
        }

        private void Fire()
        {
            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                _spawnPoints[i].gameObject.SetActive(false);

                if (TryGetInstance(out RocketMovement result))
                {
                    SetInstance(result, _spawnPoints[i]);
                }
            }
        }

        private void OnReseted()
        {
            for (int i = 1; i < _spawnPoints.Length; i++)
            {
                _spawnPoints[i].gameObject.SetActive(true);
            }
        }
    }
}