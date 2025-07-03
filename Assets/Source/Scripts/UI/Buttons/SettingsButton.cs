using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private SettingsPopup _settingsPopup;

    private Button _button;

    private void Awake()
    {
        if (_settingsPopup == null)
        {
            throw new ArgumentNullException(nameof(_settingsPopup));
        }

        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(_settingsPopup.OnOpen);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(_settingsPopup.OnOpen);
    }
}
