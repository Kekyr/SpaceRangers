using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RocketMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Coroutine _coroutine;

        private float _directionSpeed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void Start()
        {
            _directionSpeed = _speed;
        }

        public void RunRocket()
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(MoveRocket());
        }

        public void StopRocket()
        {
            _directionSpeed = 0;
        }

        private IEnumerator MoveRocket()
        {
            _directionSpeed = _speed;

            while (_directionSpeed != 0)
            {
                _rigidbody.velocity = Vector3.down * _speed * Time.fixedDeltaTime;
                yield return null;
            }
        }
    }
}