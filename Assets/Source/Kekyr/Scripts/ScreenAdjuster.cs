using System;
using UnityEngine;

public class ScreenAdjuster : MonoBehaviour
{
    private RectTransform _canvas;

    private float _width;
    private float _height;

    public Action ResolutionChanged;

    private void Start()
    {
        _width = _canvas.rect.width;
        _height = _canvas.rect.height;
    }

    private void FixedUpdate()
    {
        if (_width != _canvas.rect.width || _height != _canvas.rect.height)
        {
            _width = _canvas.rect.width;
            _height = _canvas.rect.height;
            ResolutionChanged?.Invoke();
        }
    }

    public void Init(RectTransform canvas)
    {
        _canvas = canvas;
    }
}