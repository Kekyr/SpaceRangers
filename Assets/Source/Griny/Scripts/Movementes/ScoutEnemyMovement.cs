using System;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class ScoutEnemyMovement : EnemyMovement
    {
        //private const string _borderDown = "down";
        //private const string _borderRight = "right";
        //private const string _borderLeft = "left";

        [SerializeField] private Transform _pointStart;
        [SerializeField] private Transform _pointDirectionLeft;
        [SerializeField] private Transform _pointDirecrionRight;

        [SerializeField] private Transform _pointInterectionDown;
        [SerializeField] private Transform _pointInterectionLeft;
        [SerializeField] private Transform _pointInterectionRight;

        private Vector3 _pointUp;
        //private Vector3 _pointDown;
        private Vector3 _pointLeft;
        private Vector3 _pointRight;

        private bool _isCollide = false;

        private Vector3 _currentTarget;

        public event Action DisabledEnemy;

        private void Start()
        {
            _currentTarget = GetRandomTarget();
        }

        protected override Vector2 GetVelocity(float speed)
        {
            return Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, speed * Time.deltaTime);
        }

        protected override void InteractWithWorld(float speed)
        {
            _pointUp = Camera.WorldToScreenPoint(PointInterectionUp.position);
            //_pointDown = Camera.WorldToScreenPoint(_pointInterectionDown.position);
            _pointLeft = Camera.WorldToScreenPoint(_pointInterectionLeft.position);
            _pointRight = Camera.WorldToScreenPoint(_pointInterectionRight.position);

            if(_pointUp.y >= 0)
            {
                speed = 0;
                gameObject.SetActive(false);
                _isCollide = true;
            }
            if(_pointLeft.x >= 0)
            {
                _currentTarget = _pointDirecrionRight.localPosition;
            }
            if( _pointRight.x >= Canvas.pixelRect.size.x)
            {
                _currentTarget = _pointDirectionLeft.localPosition;
            }
        }

        //protected override void CollideShip(Collider2D collider, float speed)
        //{
        //    if (collider.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
        //    {
        //        switch (backgruondBorder.GetName())
        //        {
        //            case _borderDown:
        //                speed = 0;
        //                gameObject.SetActive(false);
        //                _isCollide = true;
        //                break;
        //            case _borderRight:
        //                _currentTarget = _pointDirectionLeft.localPosition;
        //                break;
        //            case _borderLeft:
        //                _currentTarget = _pointDirecrionRight.localPosition;
        //                break;
        //        }
        //    }
        //}

        private Vector3 GetRandomTarget()
        {
            int random = UnityEngine.Random.Range(1, 3);

            if (random == 1)
            {
                return _pointDirectionLeft.localPosition;
            }
            else
            {
                return _pointDirecrionRight.localPosition;
            }
        }

        protected override void InvokeActionOutSight()
        {
            if (_isCollide == true)
            {
                DisabledEnemy?.Invoke();
                _isCollide = false;
            }
        }
    }
}