using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Bullet))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBulletMovement : MonoBehaviour
    {
        private readonly string _destructionTrigger = "Destruct";

        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Bullet _bullet;
        private Animator _animator;
        private Collider2D _collider;

        private Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _bullet = GetComponent<Bullet>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnEnable()
        {
            _direction = _bullet.Direction;
            _collider.enabled = true;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderLeft") || collider.gameObject.CompareTag("BorderRight") ||
                collider.gameObject.CompareTag("BorderDown"))
            {
                gameObject.SetActive(false);
            }

            if (collider.gameObject.CompareTag("Player"))
            {
                _collider.enabled = false;
                enabled = false;
                _rigidbody.velocity = Vector3.zero;
                _animator.SetTrigger(_destructionTrigger);
            }
        }
    }
}