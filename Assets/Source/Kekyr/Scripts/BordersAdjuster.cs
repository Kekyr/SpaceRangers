using System;
using System.Collections;
using UnityEngine;

public class BordersAdjuster : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _up;
    [SerializeField] private BoxCollider2D _left;
    [SerializeField] private BoxCollider2D _down;
    [SerializeField] private BoxCollider2D _right;
    [SerializeField] private BoxCollider2D _fighterDown;

    [SerializeField] private ScreenAdjuster _screenAdjuster;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;

    private void Awake()
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

        if (_screenAdjuster == null)
        {
            throw new ArgumentNullException(nameof(_screenAdjuster));
        }

        if (_canvas == null)
        {
            throw new ArgumentNullException(nameof(_canvas));
        }

        if (_camera == null)
        {
            throw new ArgumentNullException(nameof(_camera));
        }

        _screenAdjuster.ResolutionChanged += OnResolutionChanged;
    }

    private void OnDisable()
    {
        _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
    }

    public IEnumerator Initialization()
    {
        yield return new WaitForSeconds(0.001f);
        OnResolutionChanged();
        _screenAdjuster.enabled = true;
    }

    private void OnResolutionChanged()
    {
        Debug.Log("OnResolutionChanged!");

        RectTransform rectTransform = _canvas.GetComponent<RectTransform>();
        
        float sizeX = ((rectTransform.rect.width / 192) * 2) / 10;
        float sizeY = (rectTransform.rect.height / 192 + 2) / 10;

        _up.transform.position =
            _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.max.y));

        _left.transform.position =
            _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.min.x, _canvas.pixelRect.center.y));

        _down.transform.position =
            _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.min.y));

        _right.transform.position =
            _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.max.x, _canvas.pixelRect.center.y));

        _fighterDown.transform.position =
            _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.center.y));

        _up.size = new Vector2(sizeX, _up.size.y);
        _down.size = new Vector2(sizeX, _down.size.y);
        _fighterDown.size = new Vector2(sizeX, _fighterDown.size.y);

        _left.size = new Vector2(_left.size.x, sizeY);
        _right.size = new Vector2(_right.size.x, sizeY);
    }
}