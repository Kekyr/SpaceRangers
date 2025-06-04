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

        private int _money;

        public Action<int> Changed;

        private void Start()
        {
            if (_addSFX == null)
            {
                throw new ArgumentNullException(nameof(_addSFX));
            }
            
            _sfx = GetComponent<SFX>();
            _sfx.Init(_sfxSetting);

            _gameEndHandler.Won += OnWon;
        }

        private void OnDestroy()
        {
            _gameEndHandler.Won -= OnWon;
        }

        public void Init(AudioSettingSO sfxSetting, WalletSO data, GameEndHandler gameEndHandler, SaveLoader saveLoader)
        {
            _sfxSetting = sfxSetting;
            _data = data;
            _gameEndHandler = gameEndHandler;
            _saveLoader = saveLoader;
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
    }
}