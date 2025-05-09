using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private Score _score;
    private WinHandler _winHandler;

    private void OnEnable()
    {
        if (_textMeshPro == null)
        {
            throw new ArgumentNullException(nameof(_textMeshPro));
        }

        _score.Changed += OnChanged;
        _winHandler.Won += OnWon;
    }

    private void OnDisable()
    {
        _score.Changed -= OnChanged;
        _winHandler.Won -= OnWon;
    }

    public void Init(Score score, WinHandler winHandler)
    {
        _score = score;
        _winHandler = winHandler;
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