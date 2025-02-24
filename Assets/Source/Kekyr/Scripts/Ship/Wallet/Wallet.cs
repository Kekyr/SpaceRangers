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

        public Action<int> Changed;

        private void Awake()
        {
            if (_addSFX == null)
            {
                throw new ArgumentNullException(nameof(_addSFX));
            }
            
            _sfx = GetComponent<SFX>();
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