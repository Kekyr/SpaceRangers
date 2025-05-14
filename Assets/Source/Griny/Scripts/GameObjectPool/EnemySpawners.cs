using System;
using System.Collections.Generic;
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
    private WinPopup _winPopup;

    private int _endedCount;
    private bool _isBossSpawned;

    private void Start()
    {
        _spawners = GetComponentsInChildren<EnemySpawner>(true);

        for (int i = 0; i < _spawnersData.Count; i++)
        {
            _spawners[i].Init(_spawnersData[i], _spriteModifier, _bulletsContainer, _coinPool, _score, _screenAdjuster);
            _spawners[i].Ended += OnEnded;
        }

        _screenAdjuster.ResolutionChanged += OnResolutionChanged;
        _timer.Won += OnWon;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _spawnersData.Count; i++)
        {
            _spawners[i].Ended -= OnEnded;
        }

        _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
        _timer.Won -= OnWon;
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

    public void Init(Canvas canvas, Camera mainCamera, ScreenAdjuster screenAdjuster, WinPopup winPopup)
    {
        _canvas = canvas;
        _mainCamera = mainCamera;
        _screenAdjuster = screenAdjuster;
        _winPopup = winPopup;
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

        EnemyShip boss = _spawners[1].Initialize(_levelData.BossPrefab, _spawners[1].transform);
        _winPopup.Init(boss);
        _spawners[1].Spawn();

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