using System.Collections.Generic;
using Audio;
using Level;
using ScoreSystem;
using ShipBase;
using Tutorial;
using UnityEngine;
using WalletSystem;
using YG;

namespace SaveSystem
{
    public class SaveLoader : MonoBehaviour
    {
        private LevelsSO _levelsData;
        private WalletSO _walletData;
        private ScoreSO _scoreData;

        private AudioSettingSO _sfxSetting;
        private AudioSettingSO _musicSetting;

        private TutorialStepSO _levelTutorialData;
        private TutorialStepSO _hangarButtonTutorialData;
        private TutorialStepSO _improvementTutorialData;
        private TutorialStepSO _movementTutorialData;
        private TutorialStepSO _rocketLauncherTutorialData;
        private TutorialStepSO _winPopupTutorialData;
        private TutorialStepSO _losePopupTutorialData;

        private IImprovementsSO _bulletImprovementsData;
        private IImprovementsSO _shieldImprovementsData;
        private IImprovementsSO _shipImprovementsData;
        private IImprovementsSO _rocketImprovementsData;

        public void Init(LevelsSO levelsData, WalletSO walletData, ScoreSO scoreData, AudioSettingSO sfxSetting,
            AudioSettingSO musicSetting, TutorialStepSO levelTutorialData, TutorialStepSO hangarButtonTutorialData,
            TutorialStepSO improvementTutorialData, TutorialStepSO movementTutorialData,
            TutorialStepSO rocketLauncherTutorialData,
            TutorialStepSO winPopupTutorialData, TutorialStepSO losePopupTutorialData,
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
            _levelTutorialData = levelTutorialData;
            _hangarButtonTutorialData = hangarButtonTutorialData;
            _improvementTutorialData = improvementTutorialData;
            _movementTutorialData = movementTutorialData;
            _rocketLauncherTutorialData = rocketLauncherTutorialData;
            _winPopupTutorialData = winPopupTutorialData;
            _losePopupTutorialData = losePopupTutorialData;
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
                _levelTutorialData.IsCompleted,
                _hangarButtonTutorialData.IsCompleted,
                _improvementTutorialData.IsCompleted,
                _movementTutorialData.IsCompleted,
                _rocketLauncherTutorialData.IsCompleted,
                _winPopupTutorialData.IsCompleted,
                _losePopupTutorialData.IsCompleted,
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

            _levelTutorialData.Init(saveData.LevelTutorialCompletion);
            _hangarButtonTutorialData.Init(saveData.HangarButtonTutorialCompletion);
            _improvementTutorialData.Init(saveData.ImprovementTutorialCompletion);
            _movementTutorialData.Init(saveData.MovementTutorialCompletion);
            _rocketLauncherTutorialData.Init(saveData.RocketLauncherTutorialCompletion);
            _winPopupTutorialData.Init(saveData.WinPopupTutorialCompletion);
            _losePopupTutorialData.Init(saveData.LosePopupTutorialCompletion);

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
}