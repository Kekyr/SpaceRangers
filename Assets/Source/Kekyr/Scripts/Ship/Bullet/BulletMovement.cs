using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _speed;
        private Vector2 _direction;
        private SpriteRenderer _spriteRenderer;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        public void Init(float speed, Vector2 direction, int sortingOrder)
        {
            _speed = speed;
            _direction = direction;
            _spriteRenderer.sortingOrder = sortingOrder;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        }
    }
}