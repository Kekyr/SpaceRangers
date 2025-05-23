using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [SerializeField] private Image _blackout;

    private GameEndHandler _gameEndHandler;

    private void Start()
    {
        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }
    }

    private void OnDestroy()
    {
        _gameEndHandler.Lose -= OnLose;
    }

    public void Init(GameEndHandler gameEndHandler)
    {
        _gameEndHandler = gameEndHandler;
        _gameEndHandler.Lose += OnLose;
    }

    private void OnLose()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
}