using System;
using System.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class BordersAdjuster : MonoBehaviour
{
    private readonly float _initTime = 0.001f;

    [SerializeField] private BoxCollider2D _up;
    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _down;
    [SerializeField] private BoxCollider2D _right;
    [SerializeField] private BoxCollider2D _fighterDown;
    
    private ScreenAdjuster _screenAdjuster;
    private Camera _mainCamera;
    private Canvas _canvas;

    private int _pixelsPerUnit;

    private void Start()
    {
        if (_up == null)
        {
            throw new ArgumentNullException(nameof(_up));
        }

        if (_left == null)
        {
            throw new ArgumentNullException(nameof(_left));
        }

        if (_down == null)
        {
            throw new ArgumentNullException(nameof(_down));
        }

        if (_right == null)
        {
            throw new ArgumentNullException(nameof(_right));
        }

        if (_fighterDown == null)
        {
            throw new ArgumentNullException(nameof(_right));
        }

        _pixelsPerUnit = _mainCamera.GetComponent<PixelPerfectCamera>().assetsPPU;

        _screenAdjuster.ResolutionChanged += OnResolutionChanged;
    }

    private void OnDisable()
    {
        _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
    }

    public void Init(Canvas canvas, Camera mainCamera, ScreenAdjuster screenAdjuster)
    {
        _canvas = canvas;
        _mainCamera = mainCamera;
        _screenAdjuster = screenAdjuster;
        enabled = true;
    }

    public IEnumerator Initialization()
    {
        yield return new WaitForSeconds(_initTime);
        OnResolutionChanged();
        _screenAdjuster.enabled = true;
    }

    private void OnResolutionChanged()
    {
        int modifier = 2;

        RectTransform rectTransform = _canvas.GetComponent<RectTransform>();

        float sizeX = ((rectTransform.rect.width / _pixelsPerUnit) * modifier) / 10;
        float sizeY = (rectTransform.rect.height / _pixelsPerUnit + modifier) / 10;

        _up.transform.position =
            _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.max.y));

        _left.transform.position =
            _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.min.x, _canvas.pixelRect.center.y));

        _down.transform.position =
            _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.min.y));

        _right.transform.position =
            _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.max.x, _canvas.pixelRect.center.y));

        _fighterDown.transform.position =
            _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.center.y));

        _up.size = new Vector2(sizeX, _up.size.y);
        _down.size = new Vector2(sizeX, _down.size.y);
        _fighterDown.size = new Vector2(sizeX, _fighterDown.size.y);

        _left.size = new Vector2(_left.size.x, sizeY);
        _right.size = new Vector2(_right.size.x, sizeY);
    }
}