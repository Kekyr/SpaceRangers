using System;
using System.Collections;
using LevelEnemy;
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
    [SerializeField] private LevelsView _levelsView;
    [SerializeField] private LevelsSO _levelsData;

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

        if (_levelsView == null)
        {
            throw new ArgumentNullException(nameof(_levelsView));
        }

        if (_levelsData == null)
        {
            throw new ArgumentNullException(nameof(_levelsData));
        }
    }

    private void Awake()
    {
        Validate();

        _screenAdjuster.Init(_canvas, _camera, _background, _postProcessProfile);
        _levelsView.Init(_levelsData);

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