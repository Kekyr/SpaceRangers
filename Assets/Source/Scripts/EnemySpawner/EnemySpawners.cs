using System;
using System.Collections.Generic;
using Audio;
using Game;
using Level;
using Pool;
using ScoreSystem;
using TimerSystem;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawners : MonoBehaviour
    {
        private readonly int _minExtendWidth = 1300;

        [SerializeField] private EnemySpawner _center;

        private Canvas _canvas;
        private Camera _mainCamera;
        private ScreenAdjuster _screenAdjuster;
        private EnemySpawner[] _spawners;
        private List<EnemySpawnerSO> _spawnersData;
        private SpriteModifier _spriteModifier;
        private GameObject _bulletsContainer;
        private CoinPool _coinPool;
        private Score _score;
        private Timer _timer;
        private LevelSO _levelData;
        private GameEndHandler _gameEndHandler;
        private GameplayMusic _music;
        private AudioSettingSO _sfxSetting;

        private int _endedCount;
        private bool _isBossSpawned;
        private int _activeSpawnersCount;
        private GameObject _boss;

        public event Action Spawned;

        private void Start()
        {
            if (_center == null)
            {
                throw new ArgumentNullException(nameof(_center));
            }

            _spawners = GetComponentsInChildren<EnemySpawner>(true);

            for (int i = 0; i < _spawnersData.Count; i++)
            {
                _spawners[i].Init(_spawnersData[i], _spriteModifier, _bulletsContainer, _coinPool, _score,
                    _screenAdjuster,
                    _sfxSetting, _timer);
                _spawners[i].Ended += OnEnded;
            }

            if (_levelData.HasBoss == true)
            {
                GameObject boss = _center.Prepare(_levelData.BossPrefab, _center.transform);
                boss.transform.position = _center.transform.position;
                EnemyShip ship = boss.GetComponent<EnemyShip>();
                _gameEndHandler.Init(ship);
                _music.Init(ship);
                _boss = boss;
            }

            _screenAdjuster.ResolutionChanged += OnResolutionChanged;
            _gameEndHandler.Won += OnWon;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _spawnersData.Count; i++)
            {
                _spawners[i].Ended -= OnEnded;
            }

            _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
            _gameEndHandler.Won -= OnWon;
        }

        private void FixedUpdate()
        {
            if (_levelData.HasBoss == true && _endedCount == _activeSpawnersCount && _timer.Duration == 0)
            {
                SpawnBoss();
            }
        }

        public void Init(List<EnemySpawnerSO> spawnersData, SpriteModifier spriteModifier, GameObject bulletsContainer,
            CoinPool coinPool, Score score, Timer timer, LevelSO levelData)
        {
            _spawnersData = spawnersData;
            _spriteModifier = spriteModifier;
            _bulletsContainer = bulletsContainer;
            _coinPool = coinPool;
            _score = score;
            _timer = timer;
            _levelData = levelData;
            enabled = true;
        }

        public void Init(Canvas canvas, Camera mainCamera, ScreenAdjuster screenAdjuster, GameEndHandler gameEndHandler,
            AudioSettingSO sfxSetting, GameplayMusic music)
        {
            _canvas = canvas;
            _mainCamera = mainCamera;
            _screenAdjuster = screenAdjuster;
            _gameEndHandler = gameEndHandler;
            _music = music;
            _sfxSetting = sfxSetting;
        }

        public void OnResolutionChanged()
        {
            Vector3 firstLeftPosition;
            Vector3 secondLeftPosition;
            Vector3 firstRightPosition;
            Vector3 secondRightPosition;

            RectTransform rectTransform = _canvas.GetComponent<RectTransform>();

            if (_isBossSpawned == true)
            {
                return;
            }

            if (_canvas.pixelRect.width >= _minExtendWidth)
            {
                float oneEight = (_canvas.pixelRect.width / 100) * 12.5f;
                float threeEigth = (_canvas.pixelRect.width / 100) * 12.5f + (_canvas.pixelRect.width / 100) * 25f;

                firstLeftPosition = new Vector3(_canvas.pixelRect.min.x + threeEigth, _canvas.pixelRect.min.y,
                    _mainCamera.nearClipPlane);
                secondLeftPosition = new Vector3(_canvas.pixelRect.min.x + oneEight, _canvas.pixelRect.min.y,
                    _mainCamera.nearClipPlane);
                firstRightPosition = new Vector3(_canvas.pixelRect.max.x - threeEigth, _canvas.pixelRect.max.y,
                    _mainCamera.nearClipPlane);
                secondRightPosition = new Vector3(_canvas.pixelRect.max.x - oneEight, _canvas.pixelRect.max.y,
                    _mainCamera.nearClipPlane);

                ChangePosition(_spawners[0].transform, secondLeftPosition);
                _spawners[0].enabled = true;
                _spawners[0].Init(_spawners[1].CurrentInstanceIndex);
                _spawners[0].gameObject.SetActive(true);

                ChangePosition(_spawners[4].transform, secondRightPosition);
                _spawners[4].enabled = true;
                _spawners[4].Init(_spawners[3].CurrentInstanceIndex);
                _spawners[4].gameObject.SetActive(true);

                _activeSpawnersCount = 5;
            }
            else
            {
                float oneFifth = (_canvas.pixelRect.width / 100) * 20;

                firstLeftPosition = new Vector3(_canvas.pixelRect.min.x + oneFifth, _canvas.pixelRect.min.y,
                    _mainCamera.nearClipPlane);
                firstRightPosition = new Vector3(_canvas.pixelRect.max.x - oneFifth, _canvas.pixelRect.max.y,
                    _mainCamera.nearClipPlane);

                _spawners[0].gameObject.SetActive(false);
                _spawners[4].gameObject.SetActive(false);

                _activeSpawnersCount = 3;
            }

            ChangePosition(_spawners[1].transform, firstLeftPosition);
            _spawners[1].enabled = true;

            ChangePosition(_spawners[2].transform, _canvas.pixelRect.center);
            _spawners[2].enabled = true;

            ChangePosition(_spawners[3].transform, firstRightPosition);
            _spawners[3].enabled = true;
        }

        private void SpawnBoss()
        {
            if (_isBossSpawned == true)
            {
                return;
            }

            _boss.gameObject.SetActive(true);
            _isBossSpawned = true;
            Spawned?.Invoke();
        }

        private void ChangePosition(Transform spawner, Vector3 newScreenPosition)
        {
            Vector3 newWorldPosition = _mainCamera.ScreenToWorldPoint(newScreenPosition);
            newWorldPosition.y = spawner.position.y;
            newWorldPosition.z = 0f;
            spawner.position = newWorldPosition;
        }

        private void OnEnded()
        {
            _endedCount++;
        }

        private void OnWon()
        {
            for (int i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].gameObject.SetActive(false);
            }

            _bulletsContainer.SetActive(false);
        }
    }
}