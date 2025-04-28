using System;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class ScoutEnemyMovement : EnemyMovement
    {
        [SerializeField] private Transform _pointLeft;
        [SerializeField] private Transform _pointRight;

        private void OnEnable()
        {
            SetNewDirection(GetRandomDirection());
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            Vector3 newDirection;

            switch (collider.gameObject.tag)
            {
                case "BorderRight":
                    newDirection = (_pointLeft.transform.position - transform.position).normalized;
                    SetNewDirection(newDirection);
                    break;

                case "BorderLeft":
                    newDirection = (_pointRight.transform.position - transform.position).normalized;
                    SetNewDirection(newDirection);
                    break;
            }

            base.OnTriggerEnter2D(collider);
        }

        private Vector3 GetRandomDirection()
        {
            Vector3 destination;
            int random = UnityEngine.Random.Range(1, 3);

            if (random == 1)
            {
                destination = _pointLeft.position;
            }
            else
            {
                destination = _pointRight.position;
            }

            Vector3 newDirection = (destination - transform.position).normalized;
            return newDirection;
        }
    }
}