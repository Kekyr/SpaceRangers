using ShipBase;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public abstract class RocketSystem : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _spritsRockets;
        [SerializeField] private List<RocketLauncher> _rocketLaunchers;

        public int Number  =  0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            AttackPlayer(collision, _spritsRockets, _rocketLaunchers, Number);

            //if (collision.gameObject.TryGetComponent<ShipBase.Shield>(out var shield) || collision.gameObject.TryGetComponent<Ship>(out var ship))
            //{
            //    Debug.Log("заметил корабль");
            //    Debug.Log(_number);

            //    if (_number < _rocketLaunchers.Count)
            //    {
            //        Debug.Log("выстрел");
            //        _spritsRockets[_number].gameObject.SetActive(false);
            //        _rocketLaunchers[_number].FierRocket();
            //        _number++;
            //    }
            //    else
            //    {
            //        _number = 0;
            //    }
            //}
        }

        public void RestartRockets()
        {
            Debug.Log("рестарт");
            foreach(SpriteRenderer spriteRenderer in _spritsRockets)
            {
                spriteRenderer.gameObject.SetActive(true);
                Number = 0;
            }
        }

        protected virtual void AttackPlayer(Collider2D collider, List<SpriteRenderer> spritsRockets, List<RocketLauncher> rocketLaunchers, int number)
        {
        }
    }
}