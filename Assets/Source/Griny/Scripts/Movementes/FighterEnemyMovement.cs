using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class FighterEnemyMovement : EnemyMovement
    {
        [SerializeField] private List<Transform> _directions;

        private List<int> _upDirectionsIndex = new List<int> { 0, 1, 7 };
        private List<int> _downDirectionsIndex = new List<int> { 3, 4, 5 };
        private List<int> _leftDirectionsIndex = new List<int> { 1, 2, 3 };
        private List<int> _rightDirectionsIndex = new List<int> { 5, 6, 7 };

        private bool _isInside;

        private void OnEnable()
        {
            _isInside = false;
            SetNewDirection(Vector2.down);
        }

        protected override void OnTriggerEnter2D(Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case "BorderUp":

                    if (_isInside == true)
                    {
                        SetNewDirection(GetRandomDirection(_downDirectionsIndex));
                    }

                    break;

                case "FighterDown":
                    SetNewDirection(GetRandomDirection(_upDirectionsIndex));
                    break;

                case "BorderRight":
                    SetNewDirection(GetRandomDirection(_leftDirectionsIndex));
                    break;

                case "BorderLeft":
                    SetNewDirection(GetRandomDirection(_rightDirectionsIndex));
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderUp"))
            {
                _isInside = true;
            }
        }

        private Vector3 GetRandomDirection(List<int> directions)
        {
            int randomIndex = Random.Range(0, 3);
            Vector3 destination = _directions[directions[randomIndex]].position;
            Vector3 newDirection = (destination - transform.position).normalized;
            return newDirection;
        }
    }
}