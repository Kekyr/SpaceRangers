using System;
using System.Collections.Generic;
using UnityEngine;
using ShipBase;

namespace Enemy
{
    public class FighterNairan : MonoBehaviour
    {
        [SerializeField] private List<Rocket> _rockets;

        private int _number = 0;

        private void Awake()
        {
            if (_rockets.Count == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<Ship>(out Ship ship) ||
                collision.gameObject.TryGetComponent<EnemyShield>(out EnemyShield shield))
            {
                if (_number < _rockets.Count)
                {
                    _rockets[_number].RunRocket();
                    _number++;
                }
                else
                {
                    _number = 0;
                }
            }
        }

        public void RestartRockets()
        {
            foreach (Rocket rocket in _rockets)
            {
                rocket.Restart();
                rocket.transform.parent = gameObject.transform;
            }

            _number = 0;
        }
    }
}