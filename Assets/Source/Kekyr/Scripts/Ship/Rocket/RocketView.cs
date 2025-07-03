using ShipBase;
using TMPro;
using UnityEngine;

public class RocketView : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    private RocketLauncher _rocketLauncher;

    private void OnEnable()
    {
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        _rocketLauncher.CountChanged += OnChanged;
    }

    private void OnDisable()
    {
        _rocketLauncher.CountChanged -= OnChanged;
    }

    public void Init(RocketLauncher rocketLauncher)
    {
        _rocketLauncher = rocketLauncher;
        enabled = true;
    }

    private void OnChanged(int newValue)
    {
        _textMeshPro.text = newValue.ToString();
    }
}