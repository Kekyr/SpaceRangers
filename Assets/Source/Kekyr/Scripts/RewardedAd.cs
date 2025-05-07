using System;
using Audio;
using UnityEngine;
using YG;

[RequireComponent(typeof(SFX))]
public class RewardedAd : MonoBehaviour
{
    [SerializeField] private SFXSO _success;

    private SFX _sfx;
    private Music _music;

    public event Action Rewarded;
    public event Action Closed;

    private void OnEnable()
    {
        if (_success == null)
        {
            throw new ArgumentNullException(nameof(_success));
        }

        _sfx = GetComponent<SFX>();

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

    public void Init(Music music)
    {
        _music = music;
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