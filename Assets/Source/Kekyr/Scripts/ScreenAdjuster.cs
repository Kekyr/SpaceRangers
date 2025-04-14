using System;
using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    private Canvas _canvas;
    private Camera _mainCamera;

    private float _width;
    private float _height;

    private RectTransform _canvasRectTransform;

    public Action ResolutionChanged;

    private void Start()
    {
        _canvasRectTransform = _canvas.GetComponent<RectTransform>();
        
        _width = _canvasRectTransform.rect.width;
        _height = _canvasRectTransform.rect.height;
    }

    private void FixedUpdate()
    {
        if (_width != _canvasRectTransform.rect.width || _height != _canvasRectTransform.rect.height)
        {
            _width = _canvasRectTransform.rect.width;
            _height = _canvasRectTransform.rect.height;
            ResolutionChanged?.Invoke();
        }
    }

    public void Init(Canvas canvas, Camera mainCamera)
    {
        _canvas = canvas;
        _mainCamera = mainCamera;
    }

    public void Clamp(Transform gameObjectTransform)
    {
        Vector3 screenPosition = _mainCamera.WorldToScreenPoint(gameObjectTransform.position);
            
        Vector3 newScreenPosition = new Vector3(
            Mathf.Clamp(screenPosition.x, _canvas.pixelRect.min.x, _canvas.pixelRect.max.x),
            Mathf.Clamp(screenPosition.y, _canvas.pixelRect.min.y, _canvas.pixelRect.max.y));

        Vector3 newWorldPosition = _mainCamera.ScreenToWorldPoint(newScreenPosition);
        newWorldPosition.z = 0f;
        gameObjectTransform.position = newWorldPosition;
    }
}