using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class LevelsMapTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialHand _hand;
        [SerializeField] private Image _blackout;
        [SerializeField] private TutorialStep[] _steps;
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private Button[] _buttons;

        private TutorialStepSO[] _stepsData;
        private RectTransform _handRectTransform;
        private SaveLoader _saveLoader;
        private int _currentStepIndex;

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

            if (_buttons.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_buttons));
            }

            _handRectTransform = _hand.GetComponent<RectTransform>();

            for (int i = 0; i < _stepsData.Length; i++)
            {
                if (_stepsData[i].IsCompleted == true)
                {
                    continue;
                }

                _currentStepIndex = i;
                _hand.gameObject.SetActive(true);
                _blackout.gameObject.SetActive(true);
                SetStep(_steps[i], _stepsData[i].AnimationTrigger);
                break;
            }

            _hand.Clicked += OnClicked;
        }

        private void OnDestroy()
        {
            _hand.Clicked -= OnClicked;
        }

        public void Init(TutorialStepSO[] stepsData, SaveLoader saveLoader)
        {
            _stepsData = stepsData;
            _saveLoader = saveLoader;
            enabled = true;
        }

        private void OnClicked()
        {
            _stepsData[_currentStepIndex].Completed();
            _saveLoader.Save();

            for (int i = 0; i < _stepsData.Length; i++)
            {
                if (_stepsData[i].IsCompleted == true)
                {
                    continue;
                }

                _currentStepIndex = i;
                _hand.gameObject.SetActive(true);
                _blackout.gameObject.SetActive(true);
                SetStep(_steps[i], _stepsData[i].AnimationTrigger);
                return;
            }

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].interactable = true;
            }

            gameObject.SetActive(false);
        }

        private void SetStep(TutorialStep step, string trigger)
        {
            step.Prepare();
            _handAnimator.SetTrigger(trigger);
            _handRectTransform.anchorMin = step.Position.anchorMin;
            _handRectTransform.anchorMax = step.Position.anchorMax;
            _handRectTransform.anchoredPosition = step.Position.anchoredPosition;
        }
    }
}