using System;
using System.Collections.Generic;
using Audio;
using Enemy;
using LevelEnemy;
using UnityEngine;

public class EnemySpawners : MonoBehaviour
{
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
    private AudioSettingSO _sfxSetting;

    private int _endedCount;
    private bool _isBossSpawned;
    private GameObject _boss;

    private void Start()
    {
        _spawners = GetComponentsInChildren<EnemySpawner>(true);

        for (int i = 0; i < _spawnersData.Count; i++)
        {
            _spawners[i].Init(_spawnersData[i], _spriteModifier, _bulletsContainer, _coinPool, _score, _screenAdjuster,
                _sfxSetting, _timer);
            _spawners[i].Ended += OnEnded;
        }

        if (_levelData.HasBoss == true)
        {
            GameObject boss = _spawners[1].Prepare(_levelData.BossPrefab, _spawners[1].transform);
            boss.transform.position = _spawners[1].transform.position;
            EnemyShip ship = boss.GetComponent<EnemyShip>();
            _gameEndHandler.Init(ship);
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
        if (_levelData.HasBoss == true && _endedCount == _spawners.Length && _timer.Duration == 0)
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
        AudioSettingSO sfxSetting)
    {
        _canvas = canvas;
        _mainCamera = mainCamera;
        _screenAdjuster = screenAdjuster;
        _gameEndHandler = gameEndHandler;
        _sfxSetting = sfxSetting;
    }

    public void OnResolutionChanged()
    {
        RectTransform rectTransform = _canvas.GetComponent<RectTransform>();
        float quarter = (_canvas.pixelRect.width / 100) * 20;

        Vector3 leftPosition = new Vector3(_canvas.pixelRect.min.x + quarter, _canvas.pixelRect.min.y,
            _mainCamera.nearClipPlane);
        ChangePosition(_spawners[0].transform, leftPosition);

        ChangePosition(_spawners[1].transform, _canvas.pixelRect.center);

        Vector3 rightPosition = new Vector3(_canvas.pixelRect.max.x - quarter, _canvas.pixelRect.max.y,
            _mainCamera.nearClipPlane);
        ChangePosition(_spawners[2].transform, rightPosition);
    }

    private void SpawnBoss()
    {
        if (_isBossSpawned == true)
        {
            return;
        }

        _boss.gameObject.SetActive(true);
        _isBossSpawned = true;
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