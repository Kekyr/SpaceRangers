using System;
using System.Collections.Generic;
using Audio;
using Game;
using Pool;
using ScoreSystem;
using TimerSystem;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private readonly int _instanceCount = 3;

        private SpriteModifier _spriteModifier;
        private CoinPool _coinPool;
        private Score _score;
        private ScreenAdjuster _screenAdjuster;
        private GameObject _enemyBulletsContainer;
        private EnemySpawnerSO _data;
        private AudioSettingSO _sfxSetting;
        private Timer _timer;

        private int _currentInstanceIndex = 0;
        private bool _canSpawn = true;

        private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

        public event Action Ended;

        public int CurrentInstanceIndex => _currentInstanceIndex;

        private void Start()
        {
            foreach (GameObject type in _data.Sequence)
            {
                Initialize(type);
            }

            _timer.Ended += OnEnded;
            Spawn();
        }

        private void OnDestroy()
        {
            foreach (string key in _pools.Keys)
            {
                _pools.TryGetValue(key, out Queue<GameObject> pool);

                for (int i = 0; i < pool.Count; i++)
                {
                    GameObject enemy = pool.Dequeue();

                    EnemyShip enemyShip = enemy.GetComponent<EnemyShip>();
                    enemyShip.Annihilated -= OnAnnihilated;

                    DirectionChanger directionChanger = enemy.GetComponentInChildren<DirectionChanger>();
                    directionChanger.OutSight -= OnOutSight;
                }
            }

            _timer.Ended -= OnEnded;
        }

        public void Init(EnemySpawnerSO data, SpriteModifier spriteModifier,
            GameObject enemyBulletsContainer, CoinPool coinPool, Score score, ScreenAdjuster screenAdjuster,
            AudioSettingSO sfxSetting, Timer timer)
        {
            _data = data;
            _spriteModifier = spriteModifier;
            _enemyBulletsContainer = enemyBulletsContainer;
            _coinPool = coinPool;
            _score = score;
            _screenAdjuster = screenAdjuster;
            _sfxSetting = sfxSetting;
            _timer = timer;
        }

        public void Init(int currentInstanceIndex)
        {
            _currentInstanceIndex = currentInstanceIndex;
        }

        public void Initialize(GameObject prefab)
        {
            string key = prefab.name + "(Clone)";

            if (_pools.ContainsKey(key) == true)
            {
                return;
            }

            Queue<GameObject> pool = new Queue<GameObject>();
            GameObject container = new GameObject(prefab.name);
            container.transform.parent = transform;

            for (int i = 0; i < _instanceCount; i++)
            {
                pool.Enqueue(Prepare(prefab, container.transform));
            }

            _pools.Add(key, pool);
        }

        public GameObject Prepare(GameObject prefab, Transform spawnPoint)
        {
            GameObject instance = Instantiate(prefab, spawnPoint);
            instance.SetActive(false);

            SFX sfx = instance.GetComponent<SFX>();
            sfx.Init(_sfxSetting);

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
            ship.Annihilated += OnAnnihilated;

            DirectionChanger directionChanger = instance.GetComponentInChildren<DirectionChanger>();
            directionChanger.OutSight += OnOutSight;

            return instance;
        }

        public void Spawn()
        {
            GameObject enemy;

            if (_canSpawn == false || _currentInstanceIndex >= _data.Sequence.Count)
            {
                Ended?.Invoke();
                return;
            }

            string enemyType = _data.Sequence[_currentInstanceIndex].name;
            string key = enemyType + "(Clone)";
            _pools.TryGetValue(key, out Queue<GameObject> pool);
            enemy = pool.Dequeue();

            enemy.gameObject.SetActive(true);
            EnemyShip enemyShip = enemy.GetComponent<EnemyShip>();
            enemyShip.Reset();

            enemy.transform.position = transform.position;
            _currentInstanceIndex++;
        }

        private void OnEnded()
        {
            _canSpawn = false;
        }

        private void OnAnnihilated(EnemyShip ship)
        {
            if (_pools.ContainsKey(ship.gameObject.name) == true)
            {
                _pools.TryGetValue(ship.gameObject.name, out Queue<GameObject> pool);
                pool.Enqueue(ship.gameObject);
            }

            Spawn();
            _coinPool.Spawn(ship);
            _score.Add(ship);
        }

        private void OnOutSight(GameObject ship)
        {
            if (_pools.ContainsKey(ship.name) == true)
            {
                _pools.TryGetValue(ship.name, out Queue<GameObject> pool);
                pool.Enqueue(ship);
            }
            
            Spawn();
        }
    }
}