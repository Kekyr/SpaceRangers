using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        private SpriteModifier _spriteModifier;
        private Transform _enemyBulletsContainer;
        private CoinPool _coinPool;
        private Score _score;

        private EnemySpawnerSO _data;
        private List<GameObject> _pool = new List<GameObject>();
        private int _currentInstanceIndex = 0;
        private List<Gun> _guns = new List<Gun>();

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

                EnemyMovement enemyMovement = instance.GetComponent<EnemyMovement>();

                switch (enemyMovement)
                {
                    case FighterEnemyMovement:
                        break;

                    case ScoutEnemyMovement:
                        ScoutEnemyMovement scoutEnemyMovement = (ScoutEnemyMovement)enemyMovement;
                        scoutEnemyMovement.DisabledEnemy -= Spawn;
                        break;

                    case EnemyMovement:
                        enemyMovement.OutSight -= Spawn;
                        break;
                }
            }
        }

        public void Init(EnemySpawnerSO data, SpriteModifier spriteModifier, Transform enemyBulletsContainer, CoinPool coinPool, Score score)
        {
            _data = data;
            _spriteModifier = spriteModifier;
            _enemyBulletsContainer = enemyBulletsContainer;
            _coinPool = coinPool;
            _score = score;
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
                enemyShip.Annihilated += _coinPool.Spawn;
                enemyShip.Annihilated += _score.Add;

                _guns.AddRange(instance.GetComponentsInChildren<Gun>());

                foreach (Gun gun in _guns)
                {
                    gun.Init(_enemyBulletsContainer);
                }

                EnemyMovement enemyMovement = instance.GetComponent<EnemyMovement>();

                switch (enemyMovement)
                {
                    case FighterEnemyMovement:
                        break;

                    case ScoutEnemyMovement:
                        ScoutEnemyMovement scoutEnemyMovement = (ScoutEnemyMovement)enemyMovement;
                        scoutEnemyMovement.DisabledEnemy += Spawn;
                        break;

                    case EnemyMovement:
                        enemyMovement.OutSight += Spawn;
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

            if (enemy.GetComponent<FighterNairan>())
            {
                enemy.GetComponent<FighterNairan>().RestartRockets();
            }

            enemy.gameObject.SetActive(true);
            enemy.transform.position = _spawnPoint.position;
            _currentInstanceIndex++;

            enemy.GetComponent<EnemyHealth>().ResetHealth();

            if (enemy.TryGetComponent(out EnemyShield shield))
            {
                shield.ReStartValue();
            }
        }
    }
}