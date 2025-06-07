using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TutorialHand _hand;
    [SerializeField] private RectTransform[] _positions;
    [SerializeField] private Animator _handAnimator;

    private RectTransform _handRectTransform;
    private TutorialSO _tutorialData;

    private void Start()
    {
        if (_hand == null)
        {
            throw new ArgumentNullException(nameof(_hand));
        }

        if (_positions.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_positions));
        }

        if (_handAnimator == null)
        {
            throw new ArgumentNullException(nameof(_handAnimator));
        }

        if (_tutorialData.IsFinished == true)
        {
            return;
        }

        _handRectTransform = _hand.GetComponent<RectTransform>();
        SetStep(_positions[_tutorialData.CurrentIndex], _tutorialData.Current.AnimationTrigger);

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

        SetStep(_positions[_tutorialData.CurrentIndex], _tutorialData.Current.AnimationTrigger);
    }

    private void SetStep(RectTransform newPosition, string _trigger)
    {
        _handAnimator.SetTrigger(_trigger);
        _handRectTransform.anchorMin = newPosition.anchorMin;
        _handRectTransform.anchorMax = newPosition.anchorMax;
        _handRectTransform.anchoredPosition = newPosition.anchoredPosition;
    }
}