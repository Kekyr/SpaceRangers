using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SetRockets : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _spritsRockets;
        [SerializeField] private List<RocketLauncher> _rocketLaunchers;

        private int _number = 0;

        public void DisablSprite(int number)
        {
            _spritsRockets[number].gameObject.SetActive(false);
        }

        public void FireRocket(int number)
        {
            _rocketLaunchers[number].SetRocket();
        }

        public List<RocketLauncher> GetListRocket()
        {
            return _rocketLaunchers;
        }

        public int GetCountRocket()
        {
            return _rocketLaunchers.Count;
        }

        public void UpNumberShot()
        {
            _number++;
        }

        public int GetNumber()
        {
            return _number;
        }


        public void RestartRockets()
        {
            Debug.Log("рестарт");
            foreach (SpriteRenderer spriteRenderer in _spritsRockets)
            {
                spriteRenderer.gameObject.SetActive(true);
                _number = 0;
            }
        }
    }
}