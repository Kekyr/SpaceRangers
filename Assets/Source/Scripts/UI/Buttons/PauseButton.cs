using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private PausePopup _pausePopup;

    private Button _button;

    private void Awake()
    {
        if (_pausePopup == null)
        {
            throw new ArgumentNullException(nameof(_pausePopup));
        }

        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(_pausePopup.OnOpen);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(_pausePopup.OnOpen);
    }
}