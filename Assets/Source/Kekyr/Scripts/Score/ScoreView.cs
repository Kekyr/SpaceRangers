using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private Score _score;

    private void OnEnable()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
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