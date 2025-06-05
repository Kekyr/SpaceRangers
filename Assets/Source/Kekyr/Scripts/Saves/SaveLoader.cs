using System.Collections.Generic;
using Audio;
using LevelEnemy;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveLoader : MonoBehaviour
{
    private LevelsSO _levelsData;
    private WalletSO _walletData;
    private ScoreSO _scoreData;

    private AudioSettingSO _sfxSetting;
    private AudioSettingSO _musicSetting;

    private IImprovementsSO _bulletImprovementsData;
    private IImprovementsSO _shieldImprovementsData;
    private IImprovementsSO _shipImprovementsData;
    private IImprovementsSO _rocketImprovementsData;

    public void Init(LevelsSO levelsData, WalletSO walletData, ScoreSO scoreData, AudioSettingSO sfxSetting,
        AudioSettingSO musicSetting,
        IImprovementsSO bulletImprovementsData,
        IImprovementsSO shieldImprovementsData,
        IImprovementsSO shipImprovementsData, IImprovementsSO rocketImprovementsData
    )
    {
        _levelsData = levelsData;
        _walletData = walletData;
        _scoreData = scoreData;
        _bulletImprovementsData = bulletImprovementsData;
        _shieldImprovementsData = shieldImprovementsData;
        _shipImprovementsData = shipImprovementsData;
        _rocketImprovementsData = rocketImprovementsData;
        _sfxSetting = sfxSetting;
        _musicSetting = musicSetting;
    }

    public void Save()
    {
        List<LevelState> levelsState = new List<LevelState>();

        foreach (LevelSO level in _levelsData.Data)
        {
            levelsState.Add(level.Status);
        }

        List<ImprovementState> bulletImprovementsState = Convert(_bulletImprovementsData.Levels);
        List<ImprovementState> shieldImprovementsState = Convert(_shieldImprovementsData.Levels);
        List<ImprovementState> shipImprovementsState = Convert(_shipImprovementsData.Levels);
        List<ImprovementState> rocketImprovementsState = Convert(_rocketImprovementsData.Levels);

        SavesYG saveData = new SavesYG(
            _levelsData.CurrentIndex,
            _bulletImprovementsData.CurrentIndex,
            _shieldImprovementsData.CurrentIndex,
            _shipImprovementsData.CurrentIndex,
            _rocketImprovementsData.CurrentIndex,
            _walletData.Money,
            _scoreData.Points,
            _levelsData.IsFirstTime,
            _sfxSetting.IsOn,
            _musicSetting.IsOn,
            levelsState,
            bulletImprovementsState,
            shieldImprovementsState,
            shipImprovementsState,
            rocketImprovementsState
        );

        YandexGame.savesData = saveData;
        YandexGame.SaveProgress();
    }

    public void OnLoaded()
    {
        SavesYG saveData;

        saveData = YandexGame.savesData;

        _levelsData.Init(saveData.LevelsState, saveData.CurrentLevelIndex);
        _walletData.Init(saveData.Money);
        _scoreData.Init(saveData.Score);

        _sfxSetting.Init(saveData.IsSfxOn);
        _musicSetting.Init(saveData.IsMusicOn);

        _bulletImprovementsData.Init(saveData.BulletImprovementStates, saveData.CurrentBulletIndex);
        _shieldImprovementsData.Init(saveData.ShieldImprovementStates, saveData.CurrentShieldIndex);
        _shipImprovementsData.Init(saveData.ShipImprovementStates, saveData.CurrentShipIndex);
        _rocketImprovementsData.Init(saveData.RocketImprovementStates, saveData.CurrentRocketIndex);
    }

    private List<ImprovementState> Convert(ImprovementDataSO[] improvements)
    {
        List<ImprovementState> improvementStates = new List<ImprovementState>();

        for (int i = 0; i < improvements.Length; i++)
        {
            improvementStates.Add(improvements[i].Status);
        }

        return improvementStates;
    }
}