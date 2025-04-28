using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private SpriteModifier _spriteModifier;
        private CoinPool _coinPool;
        private Score _score;
        private ScreenAdjuster _screenAdjuster;
        private GameObject _enemyBulletsContainer;

        private EnemySpawnerSO _data;

        private List<GameObject> _pool = new List<GameObject>();

        private int _currentInstanceIndex = 0;
        
        private void Start()
        {
            Initialize(_data.Prefabs, transform);
            Spawn();
        }

        private void OnDestroy()
        {
            foreach (GameObject instance in _pool)
            {
                EnemyShip enemyShip = instance.GetComponent<EnemyShip>();
                enemyShip.Destroyed -= Spawn;
                enemyShip.Annihilated -= _coinPool.Spawn;
                enemyShip.Annihilated -= _score.Add;

                DirectionChanger directionChanger = instance.GetComponentInChildren<DirectionChanger>();
                directionChanger.OutSight -= Spawn;
            }
        }

        public void Init(EnemySpawnerSO data, SpriteModifier spriteModifier,
            GameObject enemyBulletsContainer, CoinPool coinPool, Score score, ScreenAdjuster screenAdjuster)
        {
            _data = data;
            _spriteModifier = spriteModifier;
            _enemyBulletsContainer = enemyBulletsContainer;
            _coinPool = coinPool;
            _score = score;
            _screenAdjuster = screenAdjuster;
            enabled = true;
        }

        private void Initialize(List<GameObject> prefabs, Transform spawnPoint)
        {
            foreach (GameObject prefab in prefabs)
            {
                GameObject instance = Instantiate(prefab, spawnPoint);
                instance.SetActive(false);
                
                if (instance.TryGetComponent<Gun>(out Gun gun))
                {
                    gun.Init(_enemyBulletsContainer.transform);
                }

                if (instance.TryGetComponent<RocketLauncher>(out RocketLauncher rocketLauncher))
                {
                    rocketLauncher.Init(_enemyBulletsContainer);
                }

                EnemyShip enemyShip = instance.GetComponent<EnemyShip>();
                enemyShip.Init(_spriteModifier, _screenAdjuster);
                enemyShip.Destroyed += Spawn;
                enemyShip.Annihilated += _coinPool.Spawn;
                enemyShip.Annihilated += _score.Add;
                
                DirectionChanger directionChanger = instance.GetComponentInChildren<DirectionChanger>();
                directionChanger.OutSight += Spawn;
                
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
                enemy.GetComponent<FighterNairan>().RestsrtRockets();
            }

            enemy.gameObject.SetActive(true);
            enemy.transform.position = transform.position;
            _currentInstanceIndex++;
            
            EnemyShip enemyShip = enemy.GetComponent<EnemyShip>();
            enemyShip.Reset();
        }
    }
}