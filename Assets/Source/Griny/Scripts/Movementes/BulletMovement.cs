using UnityEngine;
using WordGame;

namespace Enemy
{
    public class BulletMovement : Movement
    {
        private readonly string _leftBorder = "left";
        private readonly string _rightBorder = "right";

        [SerializeField] private Bullet _bullet;

        protected override Vector2 GetVelocity(float speed)
        {
            return _bullet.Direction * speed * Time.deltaTime;
        }

        protected override void CollideShip(Collider2D col, float speed)
        {
            base.CollideShip(col, speed);

            if (col.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
            {
                string borderName = backgruondBorder.GetName();

                if (borderName == _leftBorder || borderName == _rightBorder)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}