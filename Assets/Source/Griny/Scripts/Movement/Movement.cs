using UnityEngine;
using WordGame;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        private const string _borderDown = "down";

        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            CollideShip(collision, _speed);
        }

        private void Move()
        {
            _rigidbody.velocity = GetVelosity(_speed);
        }

        protected virtual Vector2 GetVelosity(float speed)
        {
            return Vector2.down * speed;
        }

        protected virtual void CollideShip(Collider2D collision, float speed)
        {
            if (collision.gameObject.TryGetComponent<BackgruondBorder>(out BackgruondBorder backgruondBorder))
            {
                if (backgruondBorder.GetName() == _borderDown)
                {
                    speed = 0;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}