using System;
using System.Collections;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Rocket : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        [SerializeField] private float _explosionDuration;
        
        private BoxCollider2D _collider;
        private SpriteRenderer _spriteRenderer;
        private WaitForSeconds _waitForExplosionEnd;

        public event Action Stopped; 

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
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _waitForExplosionEnd = new WaitForSeconds(_explosionDuration);
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Stopped?.Invoke();
                _collider.enabled = false;
                _spriteRenderer.enabled = false;
                StartCoroutine(Explode());
            }
        }

        private IEnumerator Explode()
        {
            _explosion.SetActive(true);
            yield return _waitForExplosionEnd;
            Destroy(gameObject);
        }
    }
}