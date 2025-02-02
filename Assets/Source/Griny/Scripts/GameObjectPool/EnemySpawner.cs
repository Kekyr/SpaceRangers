using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private SpriteModifier _spriteModifier;
        private EnemySpawnerSO _data;
        private List<GameObject> _pool = new List<GameObject>();
        private int _currentInstanceIndex = 0;
        
        private void Start()
        {
            Initialize(_data.Prefabs, _spawnPoint);
            Spawn();
        }

        private void OnDisable()
        {
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

        public void Init(EnemySpawnerSO data, SpriteModifier spriteModifier)
        {
            _data = data;
            _spriteModifier = spriteModifier;
            enabled = true;
        }

        private void Initialize(List<GameObject> prefabs, Transform spawnPoint)
        {
            foreach (GameObject prefab in prefabs)
            {
                GameObject instance = Instantiate(prefab, spawnPoint);
                instance.SetActive(false);
                EnemyShip enemyShip=instance.GetComponent<EnemyShip>();
                enemyShip.Init(_spriteModifier);
                enemyShip.Destroyed += Spawn;

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