using System.Collections.Generic;
using Enemy;
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

    private void Start()
    {
        _spawners = GetComponentsInChildren<EnemySpawner>();

        for (int i = 0; i < _spawnersData.Count; i++)
        {
            _spawners[i].Init(_spawnersData[i], _spriteModifier, _bulletsContainer, _coinPool, _score, _screenAdjuster);
        }

        _screenAdjuster.ResolutionChanged += OnResolutionChanged;
    }

    private void OnDisable()
    {
        _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
    }

    public void Init(List<EnemySpawnerSO> spawnersData, SpriteModifier spriteModifier, GameObject bulletsContainer,
        CoinPool coinPool, Score score)
    {
        _spawnersData = spawnersData;
        _spriteModifier = spriteModifier;
        _bulletsContainer = bulletsContainer;
        _coinPool = coinPool;
        _score = score;
        enabled = true;
    }

    public void Init(Canvas canvas, Camera mainCamera, ScreenAdjuster screenAdjuster)
    {
        _canvas = canvas;
        _mainCamera = mainCamera;
        _screenAdjuster = screenAdjuster;
    }

    public void OnResolutionChanged()
    {
        RectTransform rectTransform = _canvas.GetComponent<RectTransform>();
        float quarter = (_canvas.pixelRect.width / 100) * 25;

        ChangePosition(_spawners[0].transform, _canvas.pixelRect.center);

        Vector3 leftPosition = new Vector3(_canvas.pixelRect.min.x + quarter, _canvas.pixelRect.min.y,
            _mainCamera.nearClipPlane);
        ChangePosition(_spawners[1].transform, leftPosition);

        Vector3 rightPosition = new Vector3(_canvas.pixelRect.max.x - quarter, _canvas.pixelRect.max.y,
            _mainCamera.nearClipPlane);
        ChangePosition(_spawners[2].transform, rightPosition);
    }

    private void ChangePosition(Transform spawner, Vector3 newScreenPosition)
    {
        Vector3 newWorldPosition = _mainCamera.ScreenToWorldPoint(newScreenPosition);
        newWorldPosition.y = spawner.position.y;
        newWorldPosition.z = 0f;
        spawner.position = newWorldPosition;
    }
}