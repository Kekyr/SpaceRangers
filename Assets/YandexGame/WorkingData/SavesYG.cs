using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int CurrentLevelIndex = 0;
        public int CurrentBulletIndex = 0;
        public int CurrentShieldIndex = 0;
        public int CurrentShipIndex = 0;
        public int CurrentRocketIndex = 0;
        public int Money = 0;
        public int Score = 0;
        public bool IsFirstTime = true;
        public bool IsSfxOn = true;
        public bool IsMusicOn = true;

        public List<LevelState> LevelsState = null;
        public List<ImprovementState> BulletImprovementStates = null;
        public List<ImprovementState> ShieldImprovementStates = null;
        public List<ImprovementState> ShipImprovementStates = null;
        public List<ImprovementState> RocketImprovementStates = null;

        public SavesYG(int currentLevelIndex, int currentBulletIndex, int currentShieldIndex, int currentShipIndex,
            int currentRocketIndex, int money, int score,
            bool isFirstTime, bool isSfxOn, bool isMusicOn,
            List<LevelState> levelsState, List<ImprovementState> bulletImprovementStates, List<ImprovementState>
                shieldImprovementStates, List<ImprovementState> shipImprovementStates, List<ImprovementState>
                rocketImprovementStates)
        {
            CurrentLevelIndex = currentLevelIndex;
            CurrentBulletIndex = currentBulletIndex;
            CurrentShieldIndex = currentShieldIndex;
            CurrentShipIndex = currentShipIndex;
            CurrentRocketIndex = currentRocketIndex;
            Money = money;
            Score = score;
            IsFirstTime = isFirstTime;
            IsSfxOn = isSfxOn;
            IsMusicOn = isMusicOn;
            LevelsState = levelsState;
            BulletImprovementStates = bulletImprovementStates;
            ShieldImprovementStates = shieldImprovementStates;
            ShipImprovementStates = shipImprovementStates;
            RocketImprovementStates = rocketImprovementStates;
        }

        public SavesYG()
        {
        }
    }
}