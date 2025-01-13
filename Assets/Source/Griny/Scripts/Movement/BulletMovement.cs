using UnityEngine;

namespace Enemy
{
    public class BulletMovement : Movement
    {
        [SerializeField] private Bullet _bullet;

        protected override Vector2 GetVelosity(float speed)
        {
            return _bullet.Direction * speed * Time.deltaTime;
        }
    }
}