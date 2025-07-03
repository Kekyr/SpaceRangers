using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private Image _blackout;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }

        if (_closeButton == null)
        {
            throw new ArgumentNullException(nameof(_closeButton));
        }

        _closeButton.onClick.AddListener(OnClose);
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnClose);
    }

    public void OnOpen()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OnClose()
    {
        _blackout.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}