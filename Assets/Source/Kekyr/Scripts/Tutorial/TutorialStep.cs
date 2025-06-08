using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TutorialStep
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private RectTransform _position;
    [SerializeField] private bool _isInteractable;
    [SerializeField] private bool _isBlackOut;

    public RectTransform Position => _position;
    public bool IsBlackOut => _isBlackOut;

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
