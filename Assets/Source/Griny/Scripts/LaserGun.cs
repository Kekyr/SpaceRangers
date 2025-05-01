using System;
using UnityEngine;

namespace Enemy
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private Laser _laser;
        [SerializeField] private Sprite _idle;

        private Animator _animator;
        private EnemyShield _shield;
        private EnemyShip _ship;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_laser == null)
            {
                throw new ArgumentNullException(nameof(_laser));
            }

            if (_idle == null)
            {
                throw new ArgumentNullException(nameof(_idle));
            }

            _animator = GetComponent<Animator>();
            _shield = GetComponent<EnemyShield>();
            _ship = GetComponent<EnemyShip>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _ship.Reseted += OnReseted;
            _shield.Emptied += OnEmptied;
        }

        private void OnDestroy()
        {
            _ship.Reseted -= OnReseted;
            _shield.Emptied -= OnEmptied;
        }

        public void Fire()
        {
            _laser.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _laser.gameObject.SetActive(false);
        }

        private void OnReseted()
        {
            _spriteRenderer.sprite = _idle;
            _animator.enabled = false;
        }

        private void OnEmptied()
        {
            _animator.enabled = true;
        }
    }
}