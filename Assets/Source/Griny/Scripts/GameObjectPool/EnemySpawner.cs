using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private Transform _spawnPoint;

        private List<GameObject> _pool = new List<GameObject>();
        private int _currentInstanceIndex = 0;

        private void Awake()
        {
            Initialize(_prefabs, _spawnPoint);
        }

        private void OnDisable()
        {
            Debug.Log("I'm disabled!");
            foreach (GameObject instance in _pool)
            {
                instance.GetComponent<EnemyShip>().Destroyed -= Spawn;

                Movement movement = instance.GetComponent<Movement>();

                switch (movement)
                {
                    case FighterMovement:
                        break;

                    case ScoutMovement:
                        ScoutMovement scoutMovement = (ScoutMovement)movement;
                        scoutMovement.DisabledEnemy -= Spawn;
                        break;

                    case Movement:
                        movement.OutSight -= Spawn;
                        break;
                }
            }
        }

        private void Start()
        {
            Spawn();
        }

        private void Initialize(List<GameObject> prefabs, Transform spawnPoint)
        {
            Debug.Log($"prefabs ==null:{prefabs == null}");
            foreach (GameObject prefab in prefabs)
            {
                GameObject instance = Instantiate(prefab, spawnPoint);
                instance.SetActive(false);
                instance.GetComponent<EnemyShip>().Destroyed += Spawn;

                Movement movement = instance.GetComponent<Movement>();

                switch (movement)
                {
                    case FighterMovement:
                        break;

                    case ScoutMovement:
                        ScoutMovement scoutMovement = (ScoutMovement)movement;
                        scoutMovement.DisabledEnemy += Spawn;
                        break;

                    case Movement:
                        movement.OutSight += Spawn;
                        break;
                }

                _pool.Add(instance);
            }
        }

        private void Spawn()
        {
            GameObject enemy;

            if (_currentInstanceIndex >= _pool.Count)
            {
                _currentInstanceIndex = 0;
            }

            enemy = _pool[_currentInstanceIndex];
            enemy.GetComponent<Health>().ResetHealth();

            if (enemy.TryGetComponent(out Shield shield))
            {
                shield.ReStartValue();
            }

            enemy.transform.position = _spawnPoint.position;
            enemy.gameObject.SetActive(true);
            _currentInstanceIndex++;
        }
    }
}