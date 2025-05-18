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

        private List<GameObject> _instances = new List<GameObject>();

        private int _currentInstanceIndex = 0;

        public event Action Ended;

        private void Start()
        {
            foreach (GameObject prefab in _data.Prefabs)
            {
                Initialize(prefab, transform);
            }

            Spawn();
        }

        private void OnDestroy()
        {
            foreach (GameObject instance in _instances)
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

        public EnemyShip Initialize(GameObject prefab, Transform spawnPoint)
        {
            GameObject instance = Instantiate(prefab, spawnPoint);
            instance.SetActive(false);

            AutoGun autoGun = instance.GetComponentInChildren<AutoGun>();

            if (autoGun != null)
            {
                autoGun.Init(_enemyBulletsContainer.transform);
            }

            RocketLauncher rocketLauncher = instance.GetComponentInChildren<RocketLauncher>();

            if (rocketLauncher != null)
            {
                rocketLauncher.Init(_enemyBulletsContainer);
            }

            EnemyShip ship = instance.GetComponent<EnemyShip>();
            ship.Init(_spriteModifier, _screenAdjuster);
            ship.Destroyed += Spawn;
            ship.Annihilated += _coinPool.Spawn;
            ship.Annihilated += _score.Add;

            DirectionChanger directionChanger = instance.GetComponentInChildren<DirectionChanger>();
            directionChanger.OutSight += Spawn;

            _instances.Add(instance);

            return ship;
        }

        public void Spawn()
        {
            GameObject enemy;
            
            if (_currentInstanceIndex >= _instances.Count)
            {
                Ended?.Invoke();
                return;
            }

            enemy = _instances[_currentInstanceIndex];

            EnemyShip enemyShip = enemy.GetComponent<EnemyShip>();
            enemyShip.Reset();

            enemy.transform.position = transform.position;
            enemy.gameObject.SetActive(true);
            _currentInstanceIndex++;
        }
    }
}