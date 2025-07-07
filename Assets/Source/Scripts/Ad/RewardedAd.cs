using System;
using Audio;
using UnityEngine;
using YG;

namespace Ad
{
    [RequireComponent(typeof(SFX))]
    public class RewardedAd : MonoBehaviour
    {
        [SerializeField] private SFXSO _success;

        private SFX _sfx;
        private GameplayMusic _music;
        private AudioSettingSO _sfxSetting;

        public event Action Rewarded;
        public event Action Closed;

        private void OnEnable()
        {
            if (_success == null)
            {
                throw new ArgumentNullException(nameof(_success));
            }

            _sfx = GetComponent<SFX>();
            _sfx.Init(_sfxSetting);

            YandexGame.OpenVideoEvent += OnOpenCallback;
            YandexGame.RewardVideoEvent += OnRewardCallback;
            YandexGame.CloseVideoEvent += OnCloseCallback;
        }

        private void OnDisable()
        {
            YandexGame.OpenVideoEvent -= OnOpenCallback;
            YandexGame.RewardVideoEvent -= OnRewardCallback;
            YandexGame.CloseVideoEvent -= OnCloseCallback;
        }

        public void Init(GameplayMusic music, AudioSettingSO sfxSetting)
        {
            _music = music;
            _sfxSetting = sfxSetting;
            enabled = true;
        }

        public void Show()
        {
            YandexGame.RewVideoShow(0);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
            _music.Pause();
        }

        private void OnRewardCallback(int id)
        {
            Rewarded?.Invoke();
            _sfx.Play(_success);
        }

        private void OnCloseCallback()
        {
            Closed?.Invoke();
            Time.timeScale = 1;
            _music.Continue();
        }
    }
}