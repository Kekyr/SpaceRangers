using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : MonoBehaviour
{
    [SerializeField] private TutorialHand _hand;
    [SerializeField] private Image _blackout;
    [SerializeField] private GameObject _explanation;
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private Button[] _buttons;

    private TutorialStepSO _tutorialData;

    private void Start()
    {
        if (_hand == null)
        {
            throw new ArgumentNullException(nameof(_hand));
        }

        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }

        if (_explanation == null)
        {
            throw new ArgumentNullException(nameof(_explanation));
        }

        if (_handAnimator == null)
        {
            throw new ArgumentNullException(nameof(_handAnimator));
        }

        if (_buttons == null)
        {
            throw new ArgumentNullException(nameof(_buttons));
        }

        if (_tutorialData.IsCompleted == true)
        {
            return;
        }
        
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = false;
        }
        
        _hand.gameObject.SetActive(true);
        _blackout.gameObject.SetActive(true);
        _explanation.gameObject.SetActive(true);
        _handAnimator.SetTrigger(_tutorialData.AnimationTrigger);
        
        _hand.Clicked += OnClicked;
    }

    private void OnDisable()
    {
        _hand.Clicked -= OnClicked;
    }

    public void Init(TutorialStepSO tutorialData)
    {
        _tutorialData = tutorialData;
    }

    private void OnClicked()
    {
        Debug.Log("Tutorial Completed!");
        _tutorialData.Completed();
        gameObject.SetActive(false);
    }
}
