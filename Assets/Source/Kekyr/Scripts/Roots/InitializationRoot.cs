using System;
using Audio;
using LevelEnemy;
using ShipBase;
using UnityEngine;
using YG;

public class InitializationRoot : MonoBehaviour
{
    [SerializeField] private SaveLoader _saveLoader;
    [SerializeField] private LevelsSO _levelsData;
    [SerializeField] private WalletSO _walletData;
    [SerializeField] private ScoreSO _scoreData;

    [SerializeField] private AudioSettingSO _sfxSetting;
    [SerializeField] private AudioSettingSO _musicSetting;

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
        Validate();

        _saveLoader.Init(_levelsData, _walletData, _scoreData, _sfxSetting, _musicSetting, _bulletImprovementsData,
            _shieldImprovementsData, _shipImprovementsData, _rocketImprovementsData);
        _saveLoader.OnLoaded();
        YandexGame.GameReadyAPI();
    }
}