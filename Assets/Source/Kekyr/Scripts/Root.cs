using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Enemy;
using LevelEnemy;
using UnityEngine;
using UnityEngine.UI;

namespace ShipBase
{
    public class Root : MonoBehaviour
    {
        private readonly float _initTime = 0.001f;

        [SerializeField] private Camera _camera;
        [SerializeField] private AutoGunsZone _autoGunsZone;
        [SerializeField] private Button _addRocketButton;
        [SerializeField] private EnemySpawners _enemySpawners;
        [SerializeField] private SpriteModifier _spriteModifier;
        [SerializeField] private Score _score;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Music _music;
        [SerializeField] private Timer _timer;

        [SerializeField] private GameObject _enemyBulletsContainer;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private CoinPool _coinPool;

        [SerializeField] private RewardedAd _rewardedAd;

        [SerializeField] private HealthView _healthView;
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private RocketView _rocketView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private RawImage _backgroundImage;

        [SerializeField] private ImprovementsSO<GameObject> _bulletData;
        [SerializeField] private ImprovementsSO<GameObject> _shipData;
        [SerializeField] private ImprovementsSO<int> _rocketData;
        [SerializeField] private ShieldImprovementsSO _shieldData;
        [SerializeField] private BackgroundSO _backgroundData;
        [SerializeField] private LevelSO _levelData;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private BordersAdjuster _bordersAdjuster;
        [SerializeField] private ScreenAdjuster _screenAdjuster;

        private void Validate()
        {
            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_canvas == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_autoGunsZone == null)
            {
                throw new ArgumentNullException(nameof(_autoGunsZone));
            }

            if (_addRocketButton == null)
            {
                throw new ArgumentNullException(nameof(_addRocketButton));
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

            if (_wallet == null)
            {
                throw new ArgumentNullException(nameof(_wallet));
            }

            if (_music == null)
            {
                throw new ArgumentNullException(nameof(_music));
            }

            if (_timer == null)
            {
                throw new ArgumentNullException(nameof(_timer));
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

            if (_levelData == null)
            {
                throw new ArgumentOutOfRangeException(nameof(_levelData));
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
        }

        private void Awake()
        {
            Validate();

            _backgroundImage.texture = _backgroundData.CurrentTexture;

            _screenAdjuster.Init(_canvas, _camera, _backgroundImage);
            _bordersAdjuster.Init(_canvas, _camera, _screenAdjuster);

            GameObject player = Instantiate(_shipData.CurrentLevel, _playerSpawnPoint);

            PlayerInputRouter playerInputRouter = player.GetComponent<PlayerInputRouter>();
            Ship ship = player.GetComponent<Ship>();
            DamageHandler damageHandler = player.GetComponent<DamageHandler>();
            Movement[] movements = player.GetComponents<Movement>();
            ShipHealth health = player.GetComponent<ShipHealth>();
            Shield shield = player.GetComponentInChildren<Shield>();
            RocketLauncher rocketLauncher = player.GetComponentInChildren<RocketLauncher>();
            BulletPool pool = player.GetComponentInChildren<BulletPool>();

            _coinPool.Init(player.transform, health);

            if (player.TryGetComponent(out AutoGuns autoGuns))
            {
                autoGuns.Init(_autoGunsZone);
            }

            ship.Init(_wallet, _screenAdjuster);
            damageHandler.Init(_spriteModifier);

            for (int i = 0; i < movements.Length; i++)
            {
                movements[i].Init(_camera, _canvas);
            }

            rocketLauncher.Init(_camera, _addRocketButton, _rocketData.CurrentLevel, _rewardedAd);
            pool.Init(_bulletData.CurrentLevel);
            shield.Init(_shieldData.CurrentLevel);

            _timer.Init(_levelData.Duration);
            _music.Init(_timer);

            _healthView.Init(health);
            _shieldView.Init(shield);
            _walletView.Init(_wallet);
            _scoreView.Init(_score);
            _timerView.Init(_timer);

            _rocketView.Init(rocketLauncher);

            if (_rocketData.CurrentLevel == 0)
            {
                _rocketView.gameObject.SetActive(false);
            }

            _rewardedAd.Init(_music);

            List<EnemySpawnerSO> enemySpawnersData = _levelData.SpawnersData;

            _enemySpawners.Init(_canvas, _camera, _screenAdjuster);
            _enemySpawners.Init(enemySpawnersData, _spriteModifier, _enemyBulletsContainer, _coinPool,
                _score);
            
            StartCoroutine(Initialization());
        }

        private IEnumerator Initialization()
        {
            yield return new WaitForSeconds(_initTime);
            _bordersAdjuster.OnResolutionChanged();
            _screenAdjuster.ChangeBackground();
            _enemySpawners.OnResolutionChanged();
            _screenAdjuster.enabled = true;
        }
    }
}