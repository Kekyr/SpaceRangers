using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _speed;
        private Vector2 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        public void Init(float speed, Vector2 direction)
        {
            _speed = speed;
            _direction = direction;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
        }

        public void Stop()
        {
            enabled = false;
            _rigidbody.velocity = Vector3.zero;
        }
    }
}