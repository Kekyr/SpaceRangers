using Game;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(BulletMovement))]
    public class Bullet : DamageSource
    {
        private readonly string DestructionTrigger = "Destruct";

        private Animator _animator;
        private BoxCollider2D _collider;
        private BulletMovement _bulletMovement;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
            _bulletMovement = GetComponent<BulletMovement>();
        }

        private void OnEnable()
        {
            _collider.enabled = true;
            _bulletMovement.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderUp")|| 
                collider.gameObject.CompareTag("BorderLeft") || 
                collider.gameObject.CompareTag("BorderRight"))
            {
                gameObject.SetActive(false);
            }

            if (collider.gameObject.CompareTag("Enemy"))
            {
                _collider.enabled = false;
                _bulletMovement.Stop();
                _animator.SetTrigger(DestructionTrigger);
            }
        }

        private void OnDestruct()
        {
            gameObject.SetActive(false);
        }
    }
}