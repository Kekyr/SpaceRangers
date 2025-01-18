using System;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class ScoutMovement : Movement
    {
        private const string _borderDown = "down";
        private const string _borderRight = "right";
        private const string _borderLeft = "left";

        [SerializeField] private Transform _pointStart;
        [SerializeField] private Transform _pointLeft;
        [SerializeField] private Transform _pointRight;

        private bool _isCollide = false;

        public event Action DisabledEnemy;

        private Vector3 _currentTarget;

        private void Start()
        {
            _currentTarget = GetRandomTarget();
        }

        protected override Vector2 GetVelocity(float speed)
        {
            return Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, speed * Time.deltaTime);
        }

        protected override void CollideShip(Collider2D col, float speed)
        {
            if (col.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
            {
                switch (backgruondBorder.GetName())
                {
                    case _borderDown:
                        speed = 0;
                        gameObject.SetActive(false);
                        _isCollide = true;
                        break;
                    case _borderRight:
                        _currentTarget = _pointLeft.localPosition;
                        break;
                    case _borderLeft:
                        _currentTarget = _pointRight.localPosition;
                        break;
                }
            }
        }

        private Vector3 GetRandomTarget()
        {
            int random = UnityEngine.Random.Range(1, 3);

            if (random == 1)
            {
                return _pointLeft.localPosition;
            }
            else
            {
                return _pointRight.localPosition;
            }
        }

        protected override void InvokActionOutSight()
        {
            if (_isCollide == true)
            {
                DisabledEnemy?.Invoke();
                _isCollide = false;
            }
        }
    }
}