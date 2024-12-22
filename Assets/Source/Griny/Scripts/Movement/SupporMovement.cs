using UnityEngine;

namespace Enemy
{
    public class SupporMovement : MonoBehaviour
    {
        [SerializeField] private Transform _pointHealing;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private float _speed = 2;

        //protected override Vector2 GetVelosity(float speed)
        //{
        //    return Vector2.MoveTowards(transform.localPosition, _pointHealing.position, speed);
        //}

        private void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, _pointHealing.position, _speed * Time.deltaTime);
        }
    }
}