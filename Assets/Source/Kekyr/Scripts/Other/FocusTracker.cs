using Audio;
using UnityEngine;
using YG;

public class FocusTracker : MonoBehaviour
{
    private Music _music;

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

    public void Init(Music music)
    {
        _music = music;
        enabled = true;
    }

    private void OnShowWindow()
    {
        Time.timeScale = 1f;
        _music.Continue();
    }

    private void OnHideWindow()
    {
        Time.timeScale = 0f;
        _music.Pause();
    }
}
