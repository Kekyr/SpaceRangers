using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _parentBullets;

        private SpriteModifier _spriteModifier;
        private EnemySpawnerSO _data;
        private List<GameObject> _pool = new List<GameObject>();
        private int _currentInstanceIndex = 0;
        private List<Gun> _gans = new List<Gun>();
        private List<RocketLauncher> _rocketLaunchers = new List<RocketLauncher>();

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
                EnemyShip enemyShip = instance.GetComponent<EnemyShip>();
                enemyShip.Init(_spriteModifier);
                enemyShip.Destroyed += Spawn;

                _gans.AddRange(instance.GetComponentsInChildren<Gun>());

                foreach (Gun gun in _gans)
                {
                    gun.Init(_parentBullets);
                }

                _rocketLaunchers.AddRange(instance.GetComponentsInChildren<RocketLauncher>());

                foreach (RocketLauncher rocketLauncher in _rocketLaunchers)
                {
                    rocketLauncher.Init(_parentBullets);
                }

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


            enemy.gameObject.SetActive(true);
            enemy.transform.position = _spawnPoint.position;
            _currentInstanceIndex++;

            enemy.GetComponent<Health>().ResetHealth();

            if (enemy.TryGetComponent(out Shield shield))
            {
                shield.ReStartValue();
            }

            if (enemy.GetComponentInChildren<SetRockets>())
            {
                enemy.GetComponentInChildren<SetRockets>().RestartRockets();
            }
        }
    }
}