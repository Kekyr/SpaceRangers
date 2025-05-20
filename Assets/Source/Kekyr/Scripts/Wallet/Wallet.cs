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

        public Action<int> Changed;

        private void Start()
        {
            if (_addSFX == null)
            {
                throw new ArgumentNullException(nameof(_addSFX));
            }
            
            _sfx = GetComponent<SFX>();
            _sfx.Init(_sfxSetting);
        }

        public void Init(AudioSettingSO sfxSetting)
        {
            _sfxSetting = sfxSetting;
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
    }
}