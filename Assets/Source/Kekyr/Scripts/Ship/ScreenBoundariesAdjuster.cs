using System;
using UnityEngine;

public class ScreenBoundariesAdjuster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private BoxCollider2D _rightBoundary;

    private Vector2 _screenMin;
    private Vector2 _screenMax;
    private Vector2 _newPosition;

    private void Awake()
    {
        if (_camera == null)
        {
            throw new ArgumentNullException(nameof(_camera));
        }

        if (_canvas == null)
        {
            throw new ArgumentNullException(nameof(_canvas));
        }

        if (_rightBoundary == null)
        {
            throw new ArgumentNullException(nameof(_rightBoundary));
        }
    }

    private void Start()
    {
        _screenMin = _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.min.x, _canvas.pixelRect.min.y));
        _screenMax = _camera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.max.x, _canvas.pixelRect.max.y));
    }

    private void FixedUpdate()
    {
        _newPosition.x = Mathf.Clamp(transform.position.x, _screenMin.x, _screenMax.x);
        _newPosition.y = Mathf.Clamp(transform.position.y, _screenMin.y, _screenMax.y);
        transform.position = _newPosition;
    }
}