using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordGame;

namespace Enemy
{
    public class Scout : MonoBehaviour
    {
        private const string _borderDown = "down";
        private const string _borderRight = "right";
        private const string _borderLeft = "left";

        [SerializeField] private Health _health;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _pointStart;
        [SerializeField] private Transform _pointLeft;
        [SerializeField] private Transform _pointRight;

        private Vector3 _currentTarget;

        private Coroutine _corutaineMove;

        private void Start()
        {
            _currentTarget = GetRandomTarget();

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

        private Vector3 GetRandomTarget()
        {
            int random = Random.Range(1, 3);

            if(random == 1)
            {
                return _pointLeft.localPosition;
            }
            else
            {
                return _pointRight.localPosition;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<BackgruondBorder>(out BackgruondBorder backgruondBorder) == true)
            {
                if (backgruondBorder.GetName() == _borderDown)
                {
                    _speed = 0;
                    gameObject.SetActive(false);
                }
                else if(backgruondBorder.GetName() == _borderRight)
                {
                    _currentTarget = _pointLeft.localPosition;
                }
                else if(backgruondBorder.GetName() == _borderLeft)
                {
                    _currentTarget = _pointRight.localPosition;
                }
            }
        }
    }
}