using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class LevelsRoot : MonoBehaviour
{
    private readonly float _initTime = 0.001f;
    
    [SerializeField] private RawImage _background;
    [SerializeField] private ScreenAdjuster _screenAdjuster;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private PostProcessProfile _postProcessProfile;

    private void Validate()
    {
        if (_background == null)
        {
            throw new ArgumentNullException(nameof(_background));
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

        if (_postProcessProfile == null)
        {
            throw new ArgumentNullException(nameof(_postProcessProfile));
        }
    }

    private void Awake()
    {
        Validate();

        _screenAdjuster.Init(_canvas, _camera, _background, _postProcessProfile);

        StartCoroutine(Initialization());
    }
    
    private IEnumerator Initialization()
    {
        yield return new WaitForSeconds(_initTime);
        _screenAdjuster.ChangeBackground();
        _screenAdjuster.ChangeEffect();
        _screenAdjuster.enabled = true;
    }
}