using Audio;
using UnityEngine;
using YG;

public class FocusTracker : MonoBehaviour
{
    private GameplayMusic _music;
    private float _currentTimeScale;

    private void OnEnable()
    {
        YandexGame.onShowWindowGame += OnShowWindow;
        YandexGame.onHideWindowGame += OnHideWindow;
    }

    private void OnDisable()
    {
        YandexGame.onShowWindowGame -= OnShowWindow;
        YandexGame.onHideWindowGame -= OnHideWindow;
    }

    public void Init(GameplayMusic music)
    {
        _music = music;
        enabled = true;
    }

    private void OnShowWindow()
    {
        Time.timeScale = _currentTimeScale;
        _music.Continue();
    }

    private void OnHideWindow()
    {
        _currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        _music.Pause();
    }
}
