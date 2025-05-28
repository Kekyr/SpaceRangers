using System;
using System.Collections;
using Audio;
using LevelEnemy;
using ShipBase;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class LevelsRoot : MonoBehaviour
{
    private readonly float _initTime = 0.001f;

    [SerializeField] private RawImage _background;
    [SerializeField] private BackgroundSO _backgroundData;
    [SerializeField] private ScreenAdjuster _screenAdjuster;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private PostProcessProfile _postProcessProfile;
    [SerializeField] private LevelsView _levelsView;
    [SerializeField] private HangarPopup _hangarPopup;
    [SerializeField] private LevelsSO _levelsData;
    [SerializeField] private WalletSO _walletData;
    [SerializeField] private ImprovementsSO<GameObject> _bulletImprovementsData;
    [SerializeField] private ImprovementsSO<ShieldDataSO> _shieldImprovementsData;
    [SerializeField] private ImprovementsSO<GameObject> _shipImprovementsData;
    [SerializeField] private ImprovementsSO<int> _rocketImprovementsData;
    [SerializeField] private AudioSettingSO _sfxSetting;
    [SerializeField] private AudioSettingSO _musicSetting;

    [SerializeField] private SaveLoader _saveLoader;
    [SerializeField] private ResetSO _resetData;

    private void Validate()
    {
        if (_background == null)
        {
            throw new ArgumentNullException(nameof(_background));
        }

        if (_backgroundData == null)
        {
            throw new ArgumentNullException(nameof(_backgroundData));
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

        if (_hangarPopup == null)
        {
            throw new ArgumentNullException(nameof(_hangarPopup));
        }

        if (_levelsData == null)
        {
            throw new ArgumentNullException(nameof(_levelsData));
        }

        if (_walletData == null)
        {
            throw new ArgumentNullException(nameof(_walletData));
        }

        if (_bulletImprovementsData == null)
        {
            throw new ArgumentNullException(nameof(_bulletImprovementsData));
        }

        if (_shieldImprovementsData == null)
        {
            throw new ArgumentNullException(nameof(_shieldImprovementsData));
        }

        if (_shipImprovementsData == null)
        {
            throw new ArgumentNullException(nameof(_shipImprovementsData));
        }

        if (_rocketImprovementsData == null)
        {
            throw new ArgumentNullException(nameof(_rocketImprovementsData));
        }

        if (_sfxSetting == null)
        {
            throw new ArgumentNullException(nameof(_sfxSetting));
        }

        if (_musicSetting == null)
        {
            throw new ArgumentNullException(nameof(_musicSetting));
        }

        if (_saveLoader == null)
        {
            throw new ArgumentNullException(nameof(_saveLoader));
        }

        if (_resetData == null)
        {
            throw new ArgumentNullException(nameof(_resetData));
        }
    }

    private void Awake()
    {
        Validate();
        Reset();

        _saveLoader.Init(_levelsData, _walletData, _sfxSetting, _musicSetting, _bulletImprovementsData,
            _shieldImprovementsData, _shipImprovementsData, _rocketImprovementsData);
        _screenAdjuster.Init(_canvas, _camera, _background, _postProcessProfile);
        _levelsView.Init(_levelsData, _backgroundData);

        IImprovementsSO[] improvementsData = new IImprovementsSO[]
        {
            _bulletImprovementsData,
            _shieldImprovementsData,
            _shipImprovementsData,
            _rocketImprovementsData
        };

        _hangarPopup.Init(_walletData, _saveLoader, improvementsData);

        StartCoroutine(Initialization());
    }

    private IEnumerator Initialization()
    {
        yield return new WaitForSeconds(_initTime);
        _screenAdjuster.ChangeBackground();
        _screenAdjuster.ChangeEffect();
        _screenAdjuster.enabled = true;
    }

    private void Reset()
    {
        if (_resetData.IsReseted == false)
        {
            _walletData.Reset();
            _levelsData.Reset();
            _bulletImprovementsData.Reset();
            _shipImprovementsData.Reset();
            _shieldImprovementsData.Reset();
            _rocketImprovementsData.Reset();
            _resetData.Reseted();
        }
    }
}