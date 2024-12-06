using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //private Vector2[] _directions = new Vector2[] {new Vector2(0,3)}
        [SerializeField] private Transform _pointStart;

        [SerializeField] private int _distanc;

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
        }

        private void FixedUpdate()
        {
            //Vector3 direction = (-_currentTarget - _pointStart.localPosition).normalized;
            //_rigidbody.MovePosition(transform.position + direction * _speed * Time.fixedDeltaTime);
            //CheckDistanñe();

            _rigidbody.velocity = Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, _speed);

            //_rigidbody.velocity = Vector2.Lerp(_pointStart.localPosition, -_currentTarget, _speed);
        }

        private void CheckDistanñe()
        {
            //Vector3 direction = (_currentTarget - transform.position).normalized;

            //RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction, _distanc);

            //Debug.DrawRay(transform.position, direction * _distanc, Color.red, 10);
            //Debug.Log(_currentTarget);

            //if (raycastHit2D.collider == null)
            //{
            //    _rigidbody.velocity = Vector2.MoveTowards(_pointStart.localPosition, -_currentTarget, _speed);
            //}
            //else if(raycastHit2D.collider != null)
            //{
            //    //Debug.Log(raycastHit2D.collider.gameObject.name);
            //    _rigidbody.velocity = Vector2.Lerp(_pointStart.position, -raycastHit2D.collider.transform.position, _speed);               
            //}
        }


        private Vector3 GetTarget(List<int> diretions)
        {
            randomNumber = Random.Range(0, 3);
            
            return _directionsMovement[diretions[randomNumber]].localPosition;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            
            if (collider.gameObject.TryGetComponent<BackgruondBorder>(out BackgruondBorder backgruondBorder) == true)
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