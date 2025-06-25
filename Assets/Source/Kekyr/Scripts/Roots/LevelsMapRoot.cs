using System;
using System.Collections;
using Audio;
using LeaderboardBase;
using LevelEnemy;
using ShipBase;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LevelsMapRoot : MonoBehaviour
{
    private readonly float _initTime = 0.001f;

    [SerializeField] private RawImage _background;
    [SerializeField] private BackgroundSO _backgroundData;
    [SerializeField] private ScreenAdjuster _screenAdjuster;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Camera _camera;
    [SerializeField] private PostProcessLayer _postProcessLayer;
    [SerializeField] private PostProcessProfile _postProcessProfile;
    [SerializeField] private LevelsView _levelsView;
    [SerializeField] private HangarPopup _hangarPopup;
    [SerializeField] private AuthorizationPopup _authorizationPopup;
    [SerializeField] private LevelsSO _levelsData;
    [SerializeField] private WalletSO _walletData;
    [SerializeField] private TextMeshProUGUI _textMoney;
    [SerializeField] private ScoreSO _scoreData;
    [SerializeField] private ImprovementsSO<GameObject> _bulletImprovementsData;
    [SerializeField] private ImprovementsSO<ShieldDataSO> _shieldImprovementsData;
    [SerializeField] private ImprovementsSO<GameObject> _shipImprovementsData;
    [SerializeField] private ImprovementsSO<int> _rocketImprovementsData;
    [SerializeField] private Music _music;
    [SerializeField] private AudioButton _musicButton;
    [SerializeField] private AudioButton _sfxButton;
    [SerializeField] private AudioSettingSO _sfxSetting;
    [SerializeField] private AudioSettingSO _musicSetting;

    [SerializeField] private SaveLoader _saveLoader;
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private LevelsMapTutorial _mapTutorial;
    [SerializeField] private PopupTutorial _hangarPopupTutorial;
    [SerializeField] private TutorialStepSO _levelTutorialData;
    [SerializeField] private TutorialStepSO _hangarButtonTutorialData;
    [SerializeField] private TutorialStepSO _improvementTutorialData;
    [SerializeField] private TutorialStepSO _movementTutorialData;
    [SerializeField] private TutorialStepSO _rocketLauncherTutorialData;
    [SerializeField] private TutorialStepSO _winPopupTutorialData;
    [SerializeField] private TutorialStepSO _losePopupTutorialData;
    [SerializeField] private Button _resetButton;

    [SerializeField] private FocusTracker _focusTracker;

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

        if (_postProcessLayer == null)
        {
            throw new ArgumentNullException(nameof(_postProcessLayer));
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

        if (_authorizationPopup == null)
        {
            throw new ArgumentNullException(nameof(_authorizationPopup));
        }

        if (_levelsData == null)
        {
            throw new ArgumentNullException(nameof(_levelsData));
        }

        if (_walletData == null)
        {
            throw new ArgumentNullException(nameof(_walletData));
        }

        if (_textMoney == null)
        {
            throw new ArgumentNullException(nameof(_textMoney));
        }

        if (_scoreData == null)
        {
            throw new ArgumentNullException(nameof(_scoreData));
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

        if (_music == null)
        {
            throw new ArgumentNullException(nameof(_music));
        }

        if (_musicButton == null)
        {
            throw new ArgumentNullException(nameof(_musicButton));
        }

        if (_sfxButton == null)
        {
            throw new ArgumentNullException(nameof(_sfxButton));
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

        if (_leaderboard == null)
        {
            throw new ArgumentNullException(nameof(_leaderboard));
        }

        if (_mapTutorial == null)
        {
            throw new ArgumentNullException(nameof(_mapTutorial));
        }

        if (_hangarPopupTutorial == null)
        {
            throw new ArgumentNullException(nameof(_hangarPopupTutorial));
        }

        if (_levelTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_levelTutorialData));
        }

        if (_hangarButtonTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_hangarButtonTutorialData));
        }

        if (_improvementTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_improvementTutorialData));
        }

        if (_movementTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_movementTutorialData));
        }

        if (_rocketLauncherTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_rocketLauncherTutorialData));
        }

        if (_winPopupTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_winPopupTutorialData));
        }

        if (_losePopupTutorialData == null)
        {
            throw new ArgumentNullException(nameof(_losePopupTutorialData));
        }

        if (_resetButton == null)
        {
            throw new ArgumentNullException(nameof(_resetButton));
        }

        if (_focusTracker == null)
        {
            throw new ArgumentNullException(nameof(_focusTracker));
        }
    }

    private void Awake()
    {
        Validate();

        if (YandexGame.EnvironmentData.isDesktop == true)
        {
            _postProcessLayer.enabled = true;
        }

        _resetButton.onClick.AddListener(Reset);

#if UNITY_EDITOR
        _resetButton.gameObject.SetActive(true);
#endif

        _musicButton.Init(_musicSetting, _saveLoader);
        _sfxButton.Init(_sfxSetting, _saveLoader);

        TutorialStepSO[] mapTutorialsData = new[]
            { _levelTutorialData, _hangarButtonTutorialData };

        _music.Init(_musicButton, _musicSetting);
        _hangarPopupTutorial.Init(_improvementTutorialData, _saveLoader);
        _mapTutorial.Init(mapTutorialsData, _saveLoader);
        _leaderboard.Init(_scoreData);
        _saveLoader.Init(_levelsData, _walletData, _scoreData, _sfxSetting, _musicSetting, _levelTutorialData,
            _hangarButtonTutorialData, _improvementTutorialData, _movementTutorialData, _rocketLauncherTutorialData,
            _winPopupTutorialData, _losePopupTutorialData, _bulletImprovementsData,
            _shieldImprovementsData, _shipImprovementsData, _rocketImprovementsData);
        _screenAdjuster.Init(_canvas, _camera, _background, _postProcessProfile);
        _levelsView.Init(_levelsData, _backgroundData, _saveLoader);

        _focusTracker.Init(_music);

        IImprovementsSO[] improvementsData = new IImprovementsSO[]
        {
            _bulletImprovementsData,
            _shieldImprovementsData,
            _shipImprovementsData,
            _rocketImprovementsData
        };

        _hangarPopup.Init(_walletData, _saveLoader, improvementsData, _hangarPopupTutorial);
        _authorizationPopup.Init(_leaderboard);

        StartCoroutine(Initialization());

        _textMoney.text = _walletData.Money.ToString();
    }

    private void OnDestroy()
    {
        _resetButton.onClick.RemoveListener(Reset);
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
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
        YandexGame.ResetSaveProgress();
        _walletData.Reset();
        _levelsData.Reset();
        _scoreData.Reset();
        _bulletImprovementsData.Reset();
        _shipImprovementsData.Reset();
        _shieldImprovementsData.Reset();
        _rocketImprovementsData.Reset();
        _levelTutorialData.Reset();
        _hangarButtonTutorialData.Reset();
        _improvementTutorialData.Reset();
        _movementTutorialData.Reset();
        _rocketLauncherTutorialData.Reset();
        _winPopupTutorialData.Reset();
        _losePopupTutorialData.Reset();
        YandexGame.SaveProgress();
        SceneManager.LoadScene(previousSceneIndex);
    }
}