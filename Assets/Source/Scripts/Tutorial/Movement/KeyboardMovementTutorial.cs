using System;
using SaveSystem;
using ShipBase;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class KeyboardMovementTutorial : MonoBehaviour
    {
        [SerializeField] private GameObject _view;
        [SerializeField] private GameObject _explanation;
        [SerializeField] private Button _pauseButton;

        private TutorialStepSO _tutorialData;
        private Movement _movement;
        private SaveLoader _saveLoader;

        private void Start()
        {
            if (_view == null)
            {
                throw new ArgumentNullException(nameof(_view));
            }

            if (_explanation == null)
            {
                throw new ArgumentNullException(nameof(_explanation));
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
            _view.SetActive(true);
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
