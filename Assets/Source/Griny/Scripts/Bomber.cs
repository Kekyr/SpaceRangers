using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using WordGame;

namespace Enemy
{
    public class Bomber : MonoBehaviour
    {
        private const string _borderDown = "down";

        [SerializeField] private Health _health;
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Coroutine _corutaineMove;

        private void Start()
        {
            if (_corutaineMove != null)
            {
                StopCoroutine(_corutaineMove);
            }

            _corutaineMove = StartCoroutine(MoveDown());
        }

        //private void FixedUpdate()
        //{
        //    _rigidbody.velocity = 
        //}

        private IEnumerator MoveDown()
        {
            while (_speed != 0)
            {
                _rigidbody.velocity = Vector2.down * _speed;

                yield return new WaitForFixedUpdate();
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
            }
        }
    }
}

