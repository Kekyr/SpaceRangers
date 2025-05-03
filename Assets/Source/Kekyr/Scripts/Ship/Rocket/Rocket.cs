using System;
using System.Collections;
using Audio;
using Cinemachine;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(RocketMovement))]
    [RequireComponent(typeof(SFX))]
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class Rocket : MonoBehaviour
    {
        private readonly float _impulseForce = 0.05f;
        private readonly Vector3 _impulseDirection = new Vector3(1, 1, 1);
        
        [SerializeField] private GameObject _explosion;
        [SerializeField] private float _explosionDuration;
        [SerializeField] private SFXSO _explosionSFX;

        private BoxCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private RocketMovement _rocketMovement;
        private SFX _sfx;
        private CinemachineImpulseSource _impulseSource;

        private Vector3 _impulseVelocity;
        private WaitForSeconds _waitForExplosionEnd;
        private Coroutine _explode;

        public event Action<Rocket> Destroyed;

        private void Awake()
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
            _impulseSource = GetComponent<CinemachineImpulseSource>();
            _sfx = GetComponent<SFX>();

            _waitForExplosionEnd = new WaitForSeconds(_explosionDuration);
            _impulseVelocity = _impulseDirection * _impulseForce;
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
                _sfx.Play(_explosionSFX);
                _impulseSource.GenerateImpulseWithVelocity(_impulseVelocity);
                StartCoroutine(Explode());
            }

            if (collider.gameObject.CompareTag("BorderUp") && _collider.enabled == true)
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