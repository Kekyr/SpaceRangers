using System;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private Score _score;

    private void OnEnable()
    {
        if (_textMeshPro == null)
        {
            throw new ArgumentNullException(nameof(_textMeshPro));
        }

        _score.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _score.Changed -= OnChanged;
    }

    public void Init(Score score)
    {
        _score = score;
        enabled = true;
    }

    private void OnChanged(int newValue)
    {
        _textMeshPro.text = newValue.ToString();
    }
}