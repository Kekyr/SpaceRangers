using System;
using Audio;
using LevelEnemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumber;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _blackout;

    private LevelsSO _levelsData;
    private Music _music;

    private void Start()
    {
        if (_levelNumber == null)
        {
            throw new ArgumentNullException(nameof(_levelNumber));
        }

        if (_closeButton == null)
        {
            throw new ArgumentNullException(nameof(_closeButton));
        }

        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }

        _levelNumber.text = (_levelsData.CurrentIndex + 1).ToString();
        _closeButton.onClick.AddListener(OnClose);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnClose);
    }

    public void Init(Music music, LevelsSO levelsData)
    {
        _music = music;
        _levelsData = levelsData;
        enabled = true;
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