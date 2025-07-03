using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private Score _score;
    private GameEndHandler _gameEndHandler;

    private void OnEnable()
    {
        if (_textMeshPro == null)
        {
            throw new ArgumentNullException(nameof(_textMeshPro));
        }

        _score.Changed += OnChanged;
        _gameEndHandler.Won += OnWon;
    }

    private void OnDisable()
    {
        _score.Changed -= OnChanged;
        _gameEndHandler.Won -= OnWon;
    }

    public void Init(Score score, GameEndHandler gameEndHandler)
    {
        _score = score;
        _gameEndHandler = gameEndHandler;
        enabled = true;
    }

    private void OnChanged(int newValue)
    {
        _textMeshPro.text = newValue.ToString();
    }

    private void OnWon()
    {
        gameObject.SetActive(false);
    }
}