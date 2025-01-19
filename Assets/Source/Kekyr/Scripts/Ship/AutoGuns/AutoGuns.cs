using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace ShipBase
{
    public class AutoGuns : MonoBehaviour
    {
        private readonly string _isShooting = "IsShooting";

        [SerializeField] private Transform[] _guns;
        [SerializeField] private float _rotationSpeed;

        private List<GameObject> _queue = new List<GameObject>();
        private GameObject[] _spawnPoints;
        private Animator[] _animators;

        private GameObject _target;

        private AutoGunsZone _shootingZone;

        private bool _isFollowing;

        private void Start()
        {
            if (_guns.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_guns));
            }

            if (_rotationSpeed == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_rotationSpeed));
            }

            _spawnPoints = new GameObject[_guns.Length];
            _animators = new Animator[_guns.Length];

            for (int i = 0; i < _guns.Length; i++)
            {
                _spawnPoints[i] = _guns[i].transform.GetChild(0).gameObject;
                _animators[i] = _guns[i].GetComponent<Animator>();
            }

            _shootingZone.Entered += OnEnemyEntered;
            _shootingZone.Exited += OnEnemyExited;
        }

        private void OnDisable()
        {
            _shootingZone.Entered -= OnEnemyEntered;
            _shootingZone.Exited -= OnEnemyExited;
        }

        private void FixedUpdate()
        {
            if (_queue.Count != 0 && _isFollowing == false)
            {
                SetTarget();
            }
        }

        public void Init(AutoGunsZone shootingZone)
        {
            _shootingZone = shootingZone;
            enabled = true;
        }

        private void SetTarget()
        {
            int lastElementIndex = _queue.Count - 1;

            _isFollowing = true;
            _target = _queue[lastElementIndex];

            EnemyShip enemy = _target.GetComponent<EnemyShip>();
            enemy.Exited += OnTargetExited;

            _queue.Remove(_target);

            Shoot(true);
            StartCoroutine(Follow());
        }

        private void Shoot(bool canShoot)
        {
            for (int i = 0; i < _guns.Length; i++)
            {
                _animators[i].SetBool(_isShooting, canShoot);
                _spawnPoints[i].SetActive(canShoot);
            }
        }

        private IEnumerator Follow()
        {
            while (_isFollowing == true)
            {
                for (int i = 0; i < _guns.Length; i++)
                {
                    Vector3 direction = (_target.transform.position - _guns[i].position).normalized;
                    Rotate(_guns[i], direction);
                }

                yield return null;
            }
        }

        private void Rotate(Transform transform, Vector3 direction)
        {
            Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = newRotation;
        }

        private void OnTargetExited(EnemyShip enemy)
        {
            enemy.Exited -= OnTargetExited;
            _isFollowing = false;
        }

        private void OnEnemyEntered(GameObject enemy)
        {
            _queue.Insert(0, enemy);
        }

        private void OnEnemyExited(GameObject enemy)
        {
            if (_queue.Contains(enemy) == true)
            {
                _queue.Remove(enemy);
            }
        }
    }
}