using System;
using Audio;
using UnityEngine;

namespace ShipBase
{
    [RequireComponent(typeof(SFX))]
    public class Wallet : MonoBehaviour
    {
        [SerializeField] private SFXSO _addSFX;

        private SFX _sfx;
        private AudioSettingSO _sfxSetting;
        private WalletSO _data;
        private GameEndHandler _gameEndHandler;
        private SaveLoader _saveLoader;
        private RewardedAd _rewardedAd;

        private int _money;

        public Action<int> Changed;

        public int Money => _money;

        private void Start()
        {
            if (_addSFX == null)
            {
                throw new ArgumentNullException(nameof(_addSFX));
            }

            _sfx = GetComponent<SFX>();
            _sfx.Init(_sfxSetting);

            _gameEndHandler.Won += OnWon;
            _rewardedAd.Rewarded += OnRewarded;
        }

        private void OnDestroy()
        {
            _gameEndHandler.Won -= OnWon;
            _rewardedAd.Rewarded -= OnRewarded;
        }

        public void Init(AudioSettingSO sfxSetting, WalletSO data, GameEndHandler gameEndHandler, SaveLoader saveLoader,
            RewardedAd rewardedAd)
        {
            _sfxSetting = sfxSetting;
            _data = data;
            _gameEndHandler = gameEndHandler;
            _saveLoader = saveLoader;
            _rewardedAd = rewardedAd;
            enabled = true;
        }

        public void Add(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            _money += amount;
            _sfx.Play(_addSFX);
            Changed?.Invoke(_money);
        }

        private void OnWon()
        {
            _data.Add(_money);
            _saveLoader.Save();
        }
        
        private void OnRewarded()
        {
            int multiplier = 2;
        
            _data.Add(_money);
            _money *= multiplier;
            _saveLoader.Save();
        }
    }
}