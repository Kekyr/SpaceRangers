using UnityEngine;

namespace Enemy
{
    public class Bullet : Attacker
    {
        private Vector3 _direction;

        public Vector2 Direction => _direction;

        public void SetVector(Vector3 vector)
        {
            _direction = vector;
        }
    }
}