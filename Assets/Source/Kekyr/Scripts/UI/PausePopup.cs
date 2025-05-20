using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _blackout;

    private Music _music;

    private void Awake()
    {
        if (_closeButton == null)
        {
            throw new ArgumentNullException(nameof(_closeButton));
        }

        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }

        _closeButton.onClick.AddListener(OnClose);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnClose);
    }

    public void Init(Music music)
    {
        _music = music;
    }

    public void OnOpen()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
        _music.Pause();
        Time.timeScale = 0f;
    }

    private void OnClose()
    {
        _blackout.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _music.Continue();
        Time.timeScale = 1f;
    }
}