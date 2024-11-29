using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class Fighter : MonoBehaviour
    {
        private const string _borderFighter = "fighterDown";
        private const string _borderLeft = "left";
        private const string _borderRight = "right";
        private const string _borderUp = "up";
        private const string _borderDown = "down";

        [SerializeField] private Health _health;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;

        [SerializeField] private List<Transform> _directionsMovement;
        [SerializeField] private Transform _pointStart;

        private Vector3 _currentTarget;
        private Coroutine _corutaineMove;
        private int randomNumber;


        private List<int> _ups = new List<int> { 0, 1, 7 };
        private List<int> _downs = new List<int> { 3, 4, 5 };
        private List<int> _lefts = new List<int> { 1, 2, 3 };
        private List<int> _rights = new List<int> { 5, 6, 7 };

        private void Start()
        {
            _currentTarget = GetTarget(_downs);

            if (_corutaineMove != null)
            {
                StopCoroutine(_corutaineMove);
            }

            _corutaineMove = StartCoroutine(MoveDown());
        }

        private IEnumerator MoveDown()
        {
            while (_speed != 0)
            {
                _rigidbody.velocity = Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, _speed);

                yield return new WaitForFixedUpdate();
            }
        }

        //private void FixedUpdate()
        //{
        //    Vector3 direction = (-_currentTarget - _pointStart.localPosition).normalized;

        //    _rigidbody.MovePosition(transform.position + direction * _speed * Time.fixedDeltaTime);
        //}

        private Vector3 GetTarget(List<int> diretions)
        {
            randomNumber = Random.Range(0, 3);
            
            return _directionsMovement[diretions[randomNumber]].localPosition;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<BackgruondBorder>(out BackgruondBorder backgruondBorder) == true)
            {
                if (backgruondBorder.GetName() == _borderFighter)
                {
                    _currentTarget = GetTarget(_ups);
                }
                else if (backgruondBorder.GetName() == _borderLeft)
                {
                    _currentTarget = GetTarget(_rights);
                }
                else if(backgruondBorder.GetName() == _borderRight)
                {
                    _currentTarget = GetTarget(_lefts);
                }
                else if (backgruondBorder.GetName() == _borderUp)
                {
                    _currentTarget = GetTarget(_downs);
                }
                else if (backgruondBorder.GetName() == _borderDown)
                {
                    _speed = 0;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}