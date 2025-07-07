using Game;
using UnityEngine;

namespace Enemy
{
    public class Bullet : DamageSource
    {
        private Vector3 _direction;
        private EnemyBulletMovement _enemyBulletMovement;

        public Vector2 Direction => _direction;

        private void Awake()
        {
            _enemyBulletMovement = GetComponent<EnemyBulletMovement>();
        }
        
        private void OnEnable()
        {
            _enemyBulletMovement.enabled = true;
        }
        
        public void SetVector(Vector3 vector)
        {
            _direction = vector;
        }

        private void OnDestruct()
        {
            gameObject.SetActive(false);
        }
    }
}