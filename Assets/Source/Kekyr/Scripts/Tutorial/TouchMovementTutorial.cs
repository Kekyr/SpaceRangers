using System;
using System.Collections;
using ShipBase;
using UnityEngine;
using UnityEngine.UI;

public class TouchMovementTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _hand;
    [SerializeField] private GameObject _explanation;
    [SerializeField] private Animator _handAnimator;
    [SerializeField] private Button _pauseButton;

    private TutorialStepSO _tutorialData;
    private Movement _movement;

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

        if (_handAnimator == null)
        {
            throw new ArgumentNullException(nameof(_handAnimator));
        }

        if (_pauseButton == null)
        {
            throw new ArgumentNullException(nameof(_pauseButton));
        }

        if (_tutorialData.IsCompleted == true)
        {
            return;
        }
        
        Time.timeScale = 0f;
        _pauseButton.interactable = false;
        
        _handAnimator.SetTrigger(_tutorialData.AnimationTrigger);
        _hand.SetActive(true);
        _explanation.gameObject.SetActive(true);
        
        _movement.Performed += OnPerformed;
    }

    private void OnDisable()
    {
        _movement.Performed -= OnPerformed;
    }

    public void Init(TutorialStepSO tutorialData, Movement movement)
    {
        _tutorialData = tutorialData;
        _movement = movement;
        enabled = true;
    }

    private void OnPerformed()
    {
        Debug.Log("Tutorial Completed!");
        _pauseButton.interactable = true;
        _tutorialData.Completed();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}