using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using WordGame;

namespace Enemy
{
    public class FighterMovement : Movement
    {
        private const string _borderFighterDown = "fighterDown";
        private const string _borderLeft = "left";
        private const string _borderRight = "right";
        private const string _borderUp = "up";

        [SerializeField] private List<Transform> _directionsMovement;
        [SerializeField] private Transform _pointStart;

        private Vector3 _currentTarget;
        private int randomNumber;
        private int _numberDownwardDirection = 4;
        private bool _isInside = false;

        private List<int> _ups = new List<int> { 0, 1, 7 };
        private List<int> _downs = new List<int> { 3, 4, 5 };
        private List<int> _lefts = new List<int> { 1, 2, 3 };
        private List<int> _rights = new List<int> { 5, 6, 7 };

        private void Start()
        {
            _currentTarget = _directionsMovement[_numberDownwardDirection].localPosition;
        }

        //private void OnEnable()
        //{
        //    ReloadVariable();
              //Когда убьют переменную _isInside сделать false

        //}

        //private void OnDisable()
        //{
        //    ReloadVariable();
        //}

        protected override Vector2 GetVelosity(float speed)
        {
            return Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, speed);
        }

        protected override void CollideShip(Collider2D collision, float speed)
        {
            if (collision.gameObject.TryGetComponent<BackgruondBorder>(out var backgruondBorder))
            {

                switch (backgruondBorder.GetName())
                {
                    case _borderUp:

                        if(_isInside == true)
                        {
                            _currentTarget = GetTarget(_downs);
                        }

                        _isInside = true;
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

        private Vector3 GetTarget(List<int> diretions)
        {
            randomNumber = Random.Range(0, 3);

            return _directionsMovement[diretions[randomNumber]].localPosition;
        }

        private void ReloadVariable()
        {
            _isInside = false;
        }
    }
}