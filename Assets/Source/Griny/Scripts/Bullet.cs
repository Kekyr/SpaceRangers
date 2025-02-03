using UnityEngine;

namespace Enemy
{
    public class Bullet : Attacker
    {
        private Vector3 _direction;
        private BulletMovement _bulletMovement;

        public Vector2 Direction => _direction;

        private void Awake()
        {
            _bulletMovement = GetComponent<BulletMovement>();
        }
        
        private void OnEnable()
        {
            _bulletMovement.enabled = true;
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