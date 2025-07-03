using UnityEngine;

namespace Enemy
{
    public class Dreadnought : EnemyShip
    {
        [SerializeField] private DreadnoughtMovement _dreadnoughtMovement;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _dreadnoughtMovement.CollideShip(collision);
        }
    }
}