using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BulletMovement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector2.up * _speed;
        }

        public void Init(float speed)
        {
            _speed = speed;
            enabled = true;
        }
    }
}