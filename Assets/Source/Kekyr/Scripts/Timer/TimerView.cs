using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    private readonly Color _endColor = Color.green;

    private TextMeshProUGUI _textMeshPro;
    private Timer _timer;

    private void OnEnable()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _timer.Changed += OnChanged;
        _timer.Ends += OnEnds;
    }

    private void OnDisable()
    {
        _timer.Changed -= OnChanged;
        _timer.Ends -= OnEnds;
    }

    public void Init(Timer timer)
    {
        _timer = timer;
        enabled = true;
    }

    private void OnChanged(int minutes, int seconds)
    {
        string newTime = "0";
        int minSeconds = 10;
        
        newTime = seconds >= minSeconds ? newTime + minutes + ":" + seconds : newTime + minutes + ":" + "0" + seconds;

        _textMeshPro.text = newTime;
    }

    private void OnEnds()
    {
        _textMeshPro.color = _endColor;
    }
}