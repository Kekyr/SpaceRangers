using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelsMapTutorial : MonoBehaviour
{
    [SerializeField] private TutorialHand _hand;
    [SerializeField] private Image _blackout;
    [SerializeField] private TutorialStep[] _steps;
    [SerializeField] private Animator _handAnimator;

    private RectTransform _handRectTransform;
    private TutorialSO _tutorialData;

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

        if (_steps.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_steps));
        }

        if (_handAnimator == null)
        {
            throw new ArgumentNullException(nameof(_handAnimator));
        }

        if (_tutorialData.IsFinished == true)
        {
            return;
        }
        
        _hand.gameObject.SetActive(true);
        _blackout.gameObject.SetActive(true);
        
        _handRectTransform = _hand.GetComponent<RectTransform>();

        SetStep(_steps[_tutorialData.CurrentIndex], _tutorialData.Current.AnimationTrigger);

        _hand.Clicked += OnClicked;
    }

    private void OnDestroy()
    {
        _hand.Clicked -= OnClicked;
    }

    public void Init(TutorialSO tutorialData)
    {
        _tutorialData = tutorialData;
        enabled = true;
    }

    private void OnClicked()
    {
        Debug.Log("Tutorial Completed!");
        _tutorialData.Completed();
        
        if (_tutorialData.IsFinished == true)
        {
            gameObject.SetActive(false);
        }

        SetStep(_steps[_tutorialData.CurrentIndex], _tutorialData.Current.AnimationTrigger);
    }

    private void SetStep(TutorialStep step, string _trigger)
    {
        step.Prepare();
        _handAnimator.SetTrigger(_trigger);
        _handRectTransform.anchorMin = step.ImagePosition.anchorMin;
        _handRectTransform.anchorMax = step.ImagePosition.anchorMax;
        _handRectTransform.anchoredPosition = step.ImagePosition.anchoredPosition;
    }
}