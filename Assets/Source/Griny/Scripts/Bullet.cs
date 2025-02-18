using UnityEngine;

namespace Enemy
{
    public class Bullet : Attacker
    {
        private Vector3 _direction;
        private BulletEnemyMovement _bulletEnemyMovement;

        public Vector2 Direction => _direction;

        private void Awake()
        {
            _bulletEnemyMovement = GetComponent<BulletEnemyMovement>();
        }
        
        private void OnEnable()
        {
            _bulletEnemyMovement.enabled = true;
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