using System;
using Lean.Localization;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TutorialStep
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private RectTransform _imagePosition;
    [SerializeField] private RectTransform _explanationPosition;
    [SerializeField] private string _translationName;

    [SerializeField] private bool _isInteractable;
    [SerializeField] private bool _isBlackOut;
    [SerializeField] private bool _isExplanation;
    [SerializeField] private bool _isTimeStopped;

    private LeanTranslation _translation;

    public RectTransform ImagePosition => _imagePosition;
    public RectTransform ExplanationPosition => _explanationPosition;
    public bool IsBlackOut => _isBlackOut;
    public bool IsExplanation => _isExplanation;
    public bool IsTimeStopped => _isTimeStopped;
    public string Text => (string)_translation.Data;
    
    public void Prepare()
    {
        ChangeButtonState();
        Translate();
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

    private void Translate()
    {
        if (string.IsNullOrEmpty(_translationName))
        {
            return;
        }

        _translation = LeanLocalization.GetTranslation(_translationName);
    }
}