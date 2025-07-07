using System;
using SaveSystem;
using ShipBase;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class TouchMovementTutorial : MonoBehaviour
    {
        [SerializeField] private GameObject _hand;
        [SerializeField] private GameObject _explanation;
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private Button _pauseButton;

        private TutorialStepSO _tutorialData;
        private Movement _movement;
        private SaveLoader _saveLoader;

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

            _hand.SetActive(true);
            _handAnimator.SetTrigger(_tutorialData.AnimationTrigger);
            _explanation.gameObject.SetActive(true);

            _movement.Performed += OnPerformed;
        }

        private void OnDisable()
        {
            _movement.Performed -= OnPerformed;
        }

        public void Init(TutorialStepSO tutorialData, Movement movement, SaveLoader saveLoader)
        {
            _tutorialData = tutorialData;
            _movement = movement;
            _saveLoader = saveLoader;
        }

        private void OnPerformed()
        {
            _pauseButton.interactable = true;
            _tutorialData.Completed();
            Time.timeScale = 1f;
            _saveLoader.Save();
            gameObject.SetActive(false);
        }
    }
}