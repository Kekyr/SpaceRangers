using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private TutorialHand _collider;
    [SerializeField] private GameObject _explanation;
    [SerializeField] private TextMeshProUGUI _explanationText;
    [SerializeField] private Image _blackout;
    [SerializeField] private TutorialStep[] _steps;
    [SerializeField] private Animator _handAnimator;

    private RectTransform _handRectTransform;
    private RectTransform _explanationRectTransform;
    private TutorialSO _tutorialData;

    private void Start()
    {
        if (_hand == null)
        {
            throw new ArgumentNullException(nameof(_hand));
        }

        if (_explanation == null)
        {
            throw new ArgumentNullException(nameof(_explanation));
        }

        if (_explanationText == null)
        {
            throw new ArgumentNullException(nameof(_explanationText));
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
        
        _hand.SetActive(true);
        
        Time.timeScale = 1f;
        
        _handRectTransform = _hand.GetComponent<RectTransform>();
        _explanationRectTransform = _explanation.GetComponent<RectTransform>();

        SetStep(_steps[_tutorialData.CurrentIndex], _tutorialData.Current.AnimationTrigger);

        _collider.Clicked += OnClicked;
    }

    private void OnDestroy()
    {
        _collider.Clicked -= OnClicked;
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
        Time.timeScale = 1f;
        
        if (_tutorialData.IsFinished == true)
        {
            gameObject.SetActive(false);
        }

        SetStep(_steps[_tutorialData.CurrentIndex], _tutorialData.Current.AnimationTrigger);
    }

    private void SetStep(TutorialStep step, string _trigger)
    {
        step.Prepare();
        _blackout.gameObject.SetActive(step.IsBlackOut);
        _explanation.SetActive(step.IsExplanation);
        _explanationText.text = step.Text;
        _handAnimator.SetTrigger(_trigger);
        _handRectTransform.anchorMin = step.ImagePosition.anchorMin;
        _handRectTransform.anchorMax = step.ImagePosition.anchorMax;
        _handRectTransform.anchoredPosition = step.ImagePosition.anchoredPosition;
        _explanationRectTransform.anchorMin = step.ExplanationPosition.anchorMin;
        _explanationRectTransform.anchorMax = step.ExplanationPosition.anchorMax;
        _explanationRectTransform.anchoredPosition = step.ExplanationPosition.anchoredPosition;
    }
}