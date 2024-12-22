using UnityEngine;

namespace Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private Vector3 _direction;

        public int Damage => _damage;

        public Vector2 Direction => _direction;

        public void SetVector(Vector3 vector)
        {
            _direction = vector;
        }
    }
}