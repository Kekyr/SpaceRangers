using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipBase
{
    public class AutoGuns : MonoBehaviour
    {
        [SerializeField] private AutoGunZone _shootingZone;
        [SerializeField] private Transform[] _guns;

        [SerializeField] private float _rotationSpeed;

        private List<GameObject> _targets = new List<GameObject>();

        private GameObject _target;
        private Health _targetHealth;

        private Coroutine _follow;

        private bool _isNear;

        private void OnEnable()
        {
            if (_shootingZone == null)
            {
                throw new ArgumentNullException(nameof(_shootingZone));
            }

            if (_guns.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_guns));
            }

            if (_rotationSpeed == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_rotationSpeed));
            }

            _shootingZone.Entered += OnTargetEntered;
            _shootingZone.Exited += OnTargetExited;
        }

        private void OnDisable()
        {
            _shootingZone.Entered -= OnTargetEntered;
            _shootingZone.Exited -= OnTargetExited;
        }

        private void FixedUpdate()
        {
            if (_targets.Count != 0 && _follow == null)
            {
                _follow = StartCoroutine(Follow());
            }
        }

        private IEnumerator Follow()
        {
            int lastElementIndex = _targets.Count - 1;

            _target = _targets[lastElementIndex];
            _targetHealth = _target.GetComponent<Health>();
            _targets.Remove(_target);

            _isNear = true;

            while (_isNear == true && _targetHealth.IsDead == false)
            {
                for (int i = 0; i < _guns.Length; i++)
                {
                    Vector3 direction = (_target.transform.position - _guns[i].position).normalized;
                    Rotate(_guns[i], direction);
                }

                yield return null;
            }

            StartCoroutine(Return());
            _follow = null;
        }

        private IEnumerator Return()
        {
            while (_isNear == false)
            {
                for (int i = 0; i < _guns.Length; i++)
                {
                    Rotate(_guns[i], Vector2.up);
                }

                yield return null;
            }
        }

        private void Rotate(Transform transform, Vector3 direction)
        {
            Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _rotationSpeed);
        }

        private void OnTargetEntered(GameObject target)
        {
            _targets.Insert(0, target);
        }

        private void OnTargetExited(GameObject target)
        {
            if (_targets.Contains(target) == true)
            {
                _targets.Remove(target);
                return;
            }

            if (_target == target)
            {
                _isNear = false;
            }
        }
    }
}