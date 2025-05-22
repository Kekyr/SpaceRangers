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
        private int _money;
        private AudioSettingSO _sfxSetting;
        private WalletSO _data;
        private WinHandler _winHandler;

        public Action<int> Changed;

        private void Start()
        {
            if (_addSFX == null)
            {
                throw new ArgumentNullException(nameof(_addSFX));
            }
            
            _sfx = GetComponent<SFX>();
            _sfx.Init(_sfxSetting);

            _winHandler.Won += OnWon;
        }

        private void OnDestroy()
        {
            _winHandler.Won -= OnWon;
        }

        public void Init(AudioSettingSO sfxSetting, WalletSO data, WinHandler winHandler)
        {
            _sfxSetting = sfxSetting;
            _data = data;
            _winHandler = winHandler;
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
        }
    }
}