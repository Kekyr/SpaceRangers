using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using LeaderboardBase;
using LevelEnemy;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using YG;

namespace ShipBase
{
    public class Root : MonoBehaviour
    {
        private readonly float _initTime = 0.001f;

        [SerializeField] private Camera _camera;
        [SerializeField] private PostProcessLayer _postProcessLayer;
        [SerializeField] private AutoGunsZone _autoGunsZone;
        [SerializeField] private EnemySpawners _enemySpawners;
        [SerializeField] private SpriteModifier _spriteModifier;
        [SerializeField] private Score _score;
        [SerializeField] private ScoreSO _scoreData;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private WalletSO _walletData;
        [SerializeField] private GameplayMusic _music;
        [SerializeField] private Timer _timer;
        [SerializeField] private GameEndHandler _gameEndHandler;

        [SerializeField] private GameObject _enemyBulletsContainer;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private CoinPool _coinPool;

        [SerializeField] private RewardedAd _rewardedAd;
        [SerializeField] private InterstitialAd _interstitialAd;
        [SerializeField] private RewardButton _rewardButton;

        [SerializeField] private FocusTracker _focusTracker;

        [SerializeField] private HealthView _healthView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private TimerView _timerView;

        [SerializeField] private RocketView _rocketView;

        [SerializeField] private LosePopup _losePopup;
        [SerializeField] private WinPopup _winPopup;
        [SerializeField] private PausePopup _pausePopup;

        [SerializeField] private RawImage _backgroundImage;

        [SerializeField] private ImprovementsSO<GameObject> _bulletData;
        [SerializeField] private ImprovementsSO<GameObject> _shipData;
        [SerializeField] private ImprovementsSO<int> _rocketData;
        [SerializeField] private ShieldImprovementsSO _shieldData;
        [SerializeField] private BackgroundSO _backgroundData;
        [SerializeField] private LevelsSO _levelsData;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private BordersAdjuster _bordersAdjuster;
        [SerializeField] private ScreenAdjuster _screenAdjuster;

        [SerializeField] private PostProcessProfile _postProcessProfile;

        [SerializeField] private AudioButton _musicButton;
        [SerializeField] private AudioSettingSO _musicSetting;
        [SerializeField] private AudioButton _sfxButton;
        [SerializeField] private AudioSettingSO _sfxSetting;

        [SerializeField] private SaveLoader _saveLoader;
        [SerializeField] private Leaderboard _leaderboard;

        [SerializeField] private TouchMovementTutorial _touchMovementTutorial;
        [SerializeField] private KeyboardMovementTutorial _keyboardMovementTutorial;
        [SerializeField] private TutorialStepSO _movementTutorialData;

        [SerializeField] private TouchRocketLauncherTutorial _touchRocketLauncherTutorial;
        [SerializeField] private KeyboardRocketLauncherTutorial _keyboardRocketLauncherTutorial;
        [SerializeField] private TutorialStepSO _rocketLauncherTutorialData;

        [SerializeField] private PopupTutorial _winPopupTutorial;
        [SerializeField] private TutorialStepSO _winPopupTutorialData;

        [SerializeField] private PopupTutorial _losePopupTutorial;
        [SerializeField] private TutorialStepSO _losePopupTutorialData;

        [SerializeField] private TutorialStepSO _levelTutorialData;
        [SerializeField] private TutorialStepSO _hangarButtonTutorialData;
        [SerializeField] private TutorialStepSO _improvementTutorialData;

        private LevelSO _levelData;

        private void Validate()
        {
            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_postProcessLayer == null)
            {
                throw new ArgumentNullException(nameof(_postProcessLayer));
            }

            if (_canvas == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_autoGunsZone == null)
            {
                throw new ArgumentNullException(nameof(_autoGunsZone));
            }

            if (_enemySpawners == null)
            {
                throw new ArgumentNullException(nameof(_enemySpawners));
            }

            if (_spriteModifier == null)
            {
                throw new ArgumentNullException(nameof(_spriteModifier));
            }

            if (_score == null)
            {
                throw new ArgumentNullException(nameof(_score));
            }

            if (_scoreData == null)
            {
                throw new ArgumentNullException(nameof(_scoreData));
            }

            if (_wallet == null)
            {
                throw new ArgumentNullException(nameof(_wallet));
            }

            if (_walletData == null)
            {
                throw new ArgumentNullException(nameof(_walletData));
            }

            if (_music == null)
            {
                throw new ArgumentNullException(nameof(_music));
            }

            if (_timer == null)
            {
                throw new ArgumentNullException(nameof(_timer));
            }

            if (_gameEndHandler == null)
            {
                throw new ArgumentNullException(nameof(_gameEndHandler));
            }

            if (_enemyBulletsContainer == null)
            {
                throw new ArgumentNullException(nameof(_enemyBulletsContainer));
            }

            if (_playerSpawnPoint == null)
            {
                throw new ArgumentNullException(nameof(_playerSpawnPoint));
            }

            if (_coinPool == null)
            {
                throw new ArgumentNullException(nameof(_coinPool));
            }

            if (_rewardedAd == null)
            {
                throw new ArgumentNullException(nameof(_rewardedAd));
            }

            if (_interstitialAd == null)
            {
                throw new ArgumentNullException(nameof(_interstitialAd));
            }

            if (_rewardButton == null)
            {
                throw new ArgumentNullException(nameof(_rewardButton));
            }

            if (_focusTracker == null)
            {
                throw new ArgumentNullException(nameof(_focusTracker));
            }

            if (_bulletData == null)
            {
                throw new ArgumentNullException(nameof(_bulletData));
            }

            if (_shipData == null)
            {
                throw new ArgumentNullException(nameof(_shipData));
            }

            if (_rocketData == null)
            {
                throw new ArgumentNullException(nameof(_rocketData));
            }

            if (_shieldData == null)
            {
                throw new ArgumentNullException(nameof(_shieldData));
            }

            if (_backgroundData == null)
            {
                throw new ArgumentNullException(nameof(_backgroundData));
            }

            if (_levelsData == null)
            {
                throw new ArgumentNullException(nameof(_levelsData));
            }

            if (_healthView == null)
            {
                throw new ArgumentNullException(nameof(_healthView));
            }

            if (_shieldView == null)
            {
                throw new ArgumentNullException(nameof(_shieldView));
            }

            if (_walletView == null)
            {
                throw new ArgumentNullException(nameof(_walletView));
            }

            if (_scoreView == null)
            {
                throw new ArgumentNullException(nameof(_scoreView));
            }

            if (_rocketView == null)
            {
                throw new ArgumentNullException(nameof(_rocketView));
            }

            if (_timerView == null)
            {
                throw new ArgumentNullException(nameof(_timerView));
            }

            if (_losePopup == null)
            {
                throw new ArgumentNullException(nameof(_losePopup));
            }

            if (_winPopup == null)
            {
                throw new ArgumentNullException(nameof(_winPopup));
            }

            if (_pausePopup == null)
            {
                throw new ArgumentNullException(nameof(_pausePopup));
            }

            if (_backgroundImage == null)
            {
                throw new ArgumentNullException(nameof(_backgroundImage));
            }

            if (_bordersAdjuster == null)
            {
                throw new ArgumentNullException(nameof(_bordersAdjuster));
            }

            if (_screenAdjuster == null)
            {
                throw new ArgumentNullException(nameof(_screenAdjuster));
            }

            if (_postProcessProfile == null)
            {
                throw new ArgumentNullException(nameof(_postProcessProfile));
            }

            if (_musicButton == null)
            {
                throw new ArgumentNullException(nameof(_musicButton));
            }

            if (_musicSetting == null)
            {
                throw new ArgumentNullException(nameof(_musicSetting));
            }

            if (_sfxButton == null)
            {
                throw new ArgumentNullException(nameof(_sfxButton));
            }

            if (_sfxSetting == null)
            {
                throw new ArgumentNullException(nameof(_sfxSetting));
            }

            if (_saveLoader == null)
            {
                throw new ArgumentNullException(nameof(_saveLoader));
            }

            if (_leaderboard == null)
            {
                throw new ArgumentNullException(nameof(_leaderboard));
            }

            if (_touchMovementTutorial == null)
            {
                throw new ArgumentNullException(nameof(_touchMovementTutorial));
            }

            if (_keyboardMovementTutorial == null)
            {
                throw new ArgumentNullException(nameof(_keyboardMovementTutorial));
            }

            if (_movementTutorialData == null)
            {
                throw new ArgumentNullException(nameof(_movementTutorialData));
            }

            if (_touchRocketLauncherTutorial == null)
            {
                throw new ArgumentNullException(nameof(_touchRocketLauncherTutorial));
            }

            if (_keyboardRocketLauncherTutorial == null)
            {
                throw new ArgumentNullException(nameof(_keyboardRocketLauncherTutorial));
            }

            if (_rocketLauncherTutorialData == null)
            {
                throw new ArgumentNullException(nameof(_rocketLauncherTutorialData));
            }

            if (_winPopupTutorial == null)
            {
                throw new ArgumentNullException(nameof(_winPopupTutorial));
            }

            if (_winPopupTutorialData == null)
            {
                throw new ArgumentNullException(nameof(_winPopupTutorialData));
            }

            if (_losePopupTutorial == null)
            {
                throw new ArgumentNullException(nameof(_losePopupTutorial));
            }

            if (_losePopupTutorialData == null)
            {
                throw new ArgumentNullException(nameof(_losePopupTutorialData));
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
        }

        private void Awake()
        {
            Validate();

            _saveLoader.Init(_levelsData, _walletData, _scoreData, _sfxSetting, _musicSetting, _levelTutorialData,
                _hangarButtonTutorialData, _improvementTutorialData, _movementTutorialData, _rocketLauncherTutorialData,
                _winPopupTutorialData, _losePopupTutorialData, _bulletData, _shieldData,
                _shipData,
                _rocketData);

            _levelData = _levelsData.Current;

            _backgroundImage.texture = _backgroundData.CurrentTexture;

            _screenAdjuster.Init(_canvas, _camera, _backgroundImage, _postProcessProfile);
            _bordersAdjuster.Init(_canvas, _camera, _screenAdjuster);

            _focusTracker.Init(_music);

            _musicButton.Init(_musicSetting, _saveLoader);
            _sfxButton.Init(_sfxSetting, _saveLoader);

            GameObject player = Instantiate(_shipData.CurrentLevel, _playerSpawnPoint);

            Ship ship = player.GetComponent<Ship>();
            DamageHandler damageHandler = player.GetComponent<DamageHandler>();
            Movement[] movements = player.GetComponents<Movement>();
            ShipHealth health = player.GetComponent<ShipHealth>();
            Shield shield = player.GetComponentInChildren<Shield>();
            RocketLauncher rocketLauncher = player.GetComponentInChildren<RocketLauncher>();
            BulletPool pool = player.GetComponentInChildren<BulletPool>();
            TouchMovement touchMovement = player.GetComponent<TouchMovement>();
            KeyboardMovement keyboardMovement = player.GetComponent<KeyboardMovement>();

            _touchMovementTutorial.Init(_movementTutorialData, touchMovement, _saveLoader);
            _keyboardMovementTutorial.Init(_movementTutorialData, keyboardMovement, _saveLoader);

            _touchRocketLauncherTutorial.Init(_rocketLauncherTutorialData, rocketLauncher, _saveLoader);
            _keyboardRocketLauncherTutorial.Init(_rocketLauncherTutorialData, rocketLauncher, _saveLoader);

            _winPopupTutorial.Init(_winPopupTutorialData, _saveLoader);
            _losePopupTutorial.Init(_losePopupTutorialData, _saveLoader);

            _coinPool.Init(player.transform, health,_gameEndHandler);
            _gameEndHandler.Init(_timer, health, _sfxSetting, _levelData, _levelsData, _music);
            _wallet.Init(_sfxSetting, _walletData, _gameEndHandler, _saveLoader, _rewardedAd);
            _score.Init(_gameEndHandler, _scoreData, _saveLoader, _rewardedAd);

            _leaderboard.Init(_gameEndHandler, _scoreData);
            _losePopup.Init(_gameEndHandler, _losePopupTutorial);
            _winPopup.Init(_gameEndHandler, _wallet, _score, _interstitialAd, _rewardedAd, _winPopupTutorial);
            _pausePopup.Init(_music, _levelsData);

            if (player.TryGetComponent(out AutoGuns autoGuns))
            {
                autoGuns.Init(_autoGunsZone);
            }

            ship.Init(_wallet, _screenAdjuster, _sfxSetting, _gameEndHandler);
            damageHandler.Init(_spriteModifier);

            for (int i = 0; i < movements.Length; i++)
            {
                movements[i].Init(_camera, _canvas);
            }

            rocketLauncher.Init(_camera, _rocketData.CurrentLevel, _sfxSetting);
            pool.Init(_bulletData.CurrentLevel);
            shield.Init(_shieldData.CurrentLevel);

            _timerView.Init(_timer);
            _timer.Init(_levelData);
            _music.Init(_enemySpawners);
            _music.Init(_musicButton, _musicSetting);

            _healthView.Init(health);
            _shieldView.Init(shield);
            _walletView.Init(_wallet, _gameEndHandler);
            _scoreView.Init(_score, _gameEndHandler);

            _rocketView.Init(rocketLauncher);

            if (_rocketData.CurrentLevel == 0)
            {
                _rocketView.gameObject.SetActive(false);
            }

            _rewardedAd.Init(_music, _sfxSetting);
            _rewardButton.Init(_rewardedAd, _wallet);

            List<EnemySpawnerSO> enemySpawnersData = _levelData.SpawnersData;

            _enemySpawners.Init(_canvas, _camera, _screenAdjuster, _gameEndHandler, _sfxSetting, _music);
            _enemySpawners.Init(enemySpawnersData, _spriteModifier, _enemyBulletsContainer, _coinPool,
                _score, _timer, _levelData);

            StartCoroutine(Initialization());
        }

        private IEnumerator Initialization()
        {
            yield return new WaitForSeconds(_initTime);

            _bordersAdjuster.OnResolutionChanged();
            _screenAdjuster.ChangeBackground();
            _screenAdjuster.ChangeEffect();
            _enemySpawners.OnResolutionChanged();
            _screenAdjuster.enabled = true;

            if (YandexGame.EnvironmentData.isDesktop == true)
            {
                _keyboardMovementTutorial.enabled = true;
                _postProcessLayer.enabled = true;

                if (_rocketData.CurrentLevel != 0)
                {
                    _keyboardRocketLauncherTutorial.enabled = true;
                }
            }
            else
            {
                _touchMovementTutorial.enabled = true;

                if (_rocketData.CurrentLevel != 0)
                {
                    _touchRocketLauncherTutorial.enabled = true;
                }
            }
        }
    }
}