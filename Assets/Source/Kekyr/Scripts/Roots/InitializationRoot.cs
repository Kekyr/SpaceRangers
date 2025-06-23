using System;
using Audio;
using LevelEnemy;
using ShipBase;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class InitializationRoot : MonoBehaviour
{
    [SerializeField] private SaveLoader _saveLoader;
    [SerializeField] private LevelsSO _levelsData;
    [SerializeField] private WalletSO _walletData;
    [SerializeField] private ScoreSO _scoreData;

    [SerializeField] private AudioSettingSO _sfxSetting;
    [SerializeField] private AudioSettingSO _musicSetting;

    [SerializeField] private TutorialStepSO _levelTutorialData;
    [SerializeField] private TutorialStepSO _hangarButtonTutorialData;
    [SerializeField] private TutorialStepSO _improvementTutorialData;
    [SerializeField] private TutorialStepSO _movementTutorialData;
    [SerializeField] private TutorialStepSO _rocketLauncherTutorialData;
    [SerializeField] private TutorialStepSO _winPopupTutorialData;
    [SerializeField] private TutorialStepSO _losePopupTutorialData;

    [SerializeField] private ImprovementsSO<GameObject> _bulletImprovementsData;
    [SerializeField] private ImprovementsSO<ShieldDataSO> _shieldImprovementsData;
    [SerializeField] private ImprovementsSO<GameObject> _shipImprovementsData;
    [SerializeField] private ImprovementsSO<int> _rocketImprovementsData;

    private void Validate()
    {
        if (_saveLoader == null)
        {
            throw new ArgumentNullException(nameof(_saveLoader));
        }

        if (_levelsData == null)
        {
            throw new ArgumentNullException(nameof(_levelsData));
        }

        if (_walletData == null)
        {
            throw new ArgumentNullException(nameof(_walletData));
        }

        if (_scoreData == null)
        {
            throw new ArgumentNullException(nameof(_scoreData));
        }

        if (_sfxSetting == null)
        {
            throw new ArgumentNullException(nameof(_sfxSetting));
        }

        if (_musicSetting == null)
        {
            throw new ArgumentNullException(nameof(_musicSetting));
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
    }

    private void Start()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        Validate();

        _saveLoader.Init(_levelsData, _walletData, _scoreData, _sfxSetting, _musicSetting, _levelTutorialData,
            _hangarButtonTutorialData, _improvementTutorialData, _movementTutorialData, _rocketLauncherTutorialData,
            _winPopupTutorialData, _losePopupTutorialData, _bulletImprovementsData,
            _shieldImprovementsData, _shipImprovementsData, _rocketImprovementsData);
        _saveLoader.OnLoaded();
        YandexGame.GameReadyAPI();
        SceneManager.LoadScene(nextSceneIndex);
    }
}