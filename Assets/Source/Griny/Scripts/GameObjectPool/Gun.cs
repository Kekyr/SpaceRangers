using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using UnityEngine;

namespace Enemy
{
    public class Gun : MonoBehaviour
    {
        private const string _paretBullets = "ParentBullets";
        private const float _delay = 0.5f;

        [SerializeField] private SFXSO _shootSFX;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _direction;
        [SerializeField] private int _capacity;

        private Transform _parent;
        private List<Bullet> _pool = new List<Bullet>();
        private WaitForSeconds _wait = new WaitForSeconds(_delay);
        private SFX _sfx;

        private void Awake()
        {
            if (_shootSFX == null)
            {
                throw new ArgumentNullException(nameof(_shootSFX));
            }

            _sfx = GetComponentInParent<SFX>();

            Initialize();
        }

        private void OnEnable()
        {
            StartCoroutine(ShootBullet());
        }

        private void Initialize()
        {
            for (int i = 0; i < _capacity; i++)
            {
                Bullet bullet = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _parent);
                bullet.gameObject.SetActive(false);
                _pool.Add(bullet);
            }
        }

        public void Init(GameObject gameObject)
        {
            _parent = gameObject.transform;           
        }

        private IEnumerator ShootBullet()
        {
            while (gameObject.activeSelf == true)
            {
                if (TryGetInstance(out Bullet instance))
                {
                    SetInstance(instance, _spawnPoint.position);
                }

                yield return _wait;
            }
        }

        private void SetInstance(Bullet bullet, Vector3 spawnPosition)
        {
            bullet.SetVector(-_direction.transform.localPosition);
            bullet.transform.position = spawnPosition;
            bullet.transform.rotation = gameObject.transform.rotation;
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