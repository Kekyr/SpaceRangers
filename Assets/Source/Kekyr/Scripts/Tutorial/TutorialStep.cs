using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class TutorialStep
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private bool _isInteractable;
    [SerializeField] private RectTransform _position;

    public RectTransform Position => _position;
    
    public void Prepare()
    {
        ChangeButtonState();
    }

    private void ChangeButtonState()
    {
        if (_buttons.Length == 0)
        {
            return;
        }

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = _isInteractable;
        }
    }
}