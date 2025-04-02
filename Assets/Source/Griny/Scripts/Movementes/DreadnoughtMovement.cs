using System.Collections.Generic;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class DreadnoughtMovement : MonoBehaviour
    {
        private const string _borderFighterDown = "fighterDown";
        private const string _borderLeft = "left";
        private const string _borderRight = "right";
        private const string _borderUp = "up";

        [SerializeField] private List<Transform> _directionsMovement;
        [SerializeField] private Transform _pointStart;
        [SerializeField] private int _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        //[FormerlySerializedAs("_health")] [SerializeField] private EnemyHealth enemyHealth;

        private Vector3 _currentTarget;
        private int _randomNumber;
        private int _numberDownwardDirection = 4;
        private bool _isInside = false;

        private List<int> _ups = new List<int> { 0, 1, 7 };
        private List<int> _downs = new List<int> { 3, 4, 5 };
        private List<int> _lefts = new List<int> { 1, 2, 3 };
        private List<int> _rights = new List<int> { 5, 6, 7 };

        private void OnEnable()
        {
            GetStartTarget();
            ReloadVariable();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.velocity = Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, _speed * Time.deltaTime);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<BackgroundBorder>(out var backgruondBorder))
            {
                _isInside = true;
            }
        }

        public void CollideShip(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent<BackgroundBorder>(out var backgruondBorder))
            {
                switch (backgruondBorder.GetName())
                {
                    case _borderUp:

                        if (_isInside == true)
                        {
                            _currentTarget = GetTarget(_downs);
                        }

                        break;
                    case _borderFighterDown:
                        _currentTarget = GetTarget(_ups);
                        break;
                    case _borderRight:
                        _currentTarget = GetTarget(_lefts);
                        break;
                    case _borderLeft:
                        _currentTarget = GetTarget(_rights);
                        break;
                }
            }
        }

        private void GetStartTarget()
        {
            _currentTarget = _directionsMovement[_numberDownwardDirection].localPosition;
        }

        private Vector3 GetTarget(List<int> directions)
        {
            _randomNumber = Random.Range(0, 3);

            return _directionsMovement[directions[_randomNumber]].localPosition;
        }

        private void ReloadVariable()
        {
            _isInside = false;
        }
    }
}