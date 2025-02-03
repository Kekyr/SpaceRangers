using System;
using System.Collections;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(RocketMovement))]
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private float _explosionDuration;

        private BoxCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private RocketMovement _rocketMovement;

        private WaitForSeconds _waitForExplosionEnd;
        private Coroutine _explode;

        public event Action<Rocket> Destroyed;

        private void OnEnable()
        {
            if (_explosion == null)
            {
                throw new ArgumentNullException(nameof(_explosion));
            }

            if (_explosionDuration == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_explosionDuration));
            }

            _collider = GetComponent<BoxCollider2D>();
            _rocketMovement = GetComponent<RocketMovement>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _waitForExplosionEnd = new WaitForSeconds(_explosionDuration);
        }

        public void Launch()
        {
            _collider.enabled = true;
            _rocketMovement.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Enemy") && _collider.enabled == true)
            {
                _collider.enabled = false;
                _rocketMovement.Stop();
                _spriteRenderer.enabled = false;
                StartCoroutine(Explode());
            }

            if (collider.gameObject.CompareTag("Boundary") && _collider.enabled == true)
            {
                Destroyed?.Invoke(this);
                Destroy(gameObject);
            }
        }

        private IEnumerator Explode()
        {
            _explosion.SetActive(true);
            yield return _waitForExplosionEnd;
            Destroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}