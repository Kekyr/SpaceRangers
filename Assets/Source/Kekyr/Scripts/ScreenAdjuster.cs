using System;
using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private float _screenWidth;
    private float _screenHeight;

    public Action ResolutionChanged;

    private void Start()
    {
        if (_canvas == null)
        {
            throw new ArgumentNullException(nameof(_canvas));
        }

        _screenWidth = _canvas.pixelRect.width;
        _screenHeight = _canvas.pixelRect.height;
    }

    private void FixedUpdate()
    {
        if(_screenWidth != _canvas.pixelRect.width || _screenHeight != _canvas.pixelRect.height)
        {
            OnResolutionChanged();
        }
    }

    private void OnResolutionChanged()
    {
        _screenWidth = _canvas.pixelRect.width;
        _screenHeight = _canvas.pixelRect.height;
        ResolutionChanged?.Invoke();
    }
}