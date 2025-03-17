using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Audio;
using ShipBase;

namespace Enemy
{
    public class RocketLauncher : MonoBehaviour
    {
        private const string _paretBullets = "ParentBullets";
        private const float _delay = 0.5f;

        [SerializeField] private SFXSO _shootSFX;
        [SerializeField] private MovementRocket _prefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private int _capacity;

        private Transform _parent;
        private List<MovementRocket> _pool = new List<MovementRocket>();
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


        private void Initialize()
        {
            for (int i = 0; i < _capacity; i++)
            {
                MovementRocket rocket = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity, _parent);
                rocket.gameObject.SetActive(false);
                _pool.Add(rocket);
            }
        }

        public void Init(GameObject gameObject)
        {
            _parent = gameObject.transform;
        }

        //private IEnumerator ShootBullet()
        //{
        //    while (gameObject.activeSelf == true)
        //    {
        //        if (TryGetInstance(out Rocket instance))
        //        {
        //            SetInstance(instance, _spawnPoint.position);
        //        }

        //        yield return _wait;
        //    }
        //}

        private void SetInstance(MovementRocket rocket, Vector3 spawnPosition)
        {
            rocket.transform.position = spawnPosition;
            rocket.transform.rotation = gameObject.transform.rotation;
            _sfx.Play(_shootSFX);
            rocket.gameObject.SetActive(true);
            rocket.RunRocket();
        }

        private bool TryGetInstance(out MovementRocket result)
        {
            result = _pool.FirstOrDefault(instance => instance.gameObject.activeSelf == false);
            return result != null;
        }

        public void SetRocket()
        {
            if(TryGetInstance(out MovementRocket result))
            {
                SetInstance(result, gameObject.transform.position);
            }
        }
    }
}