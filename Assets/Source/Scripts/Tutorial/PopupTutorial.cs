using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{

    public class PopupTutorial : MonoBehaviour
    {
        [SerializeField] private TutorialHand _hand;
        [SerializeField] private Image _blackout;
        [SerializeField] private GameObject _explanation;
        [SerializeField] private Animator _handAnimator;
        [SerializeField] private Button[] _buttons;

        private TutorialStepSO _tutorialData;
        private SaveLoader _saveLoader;

        private void OnEnable()
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
            _hand.gameObject.SetActive(false);
            _blackout.gameObject.SetActive(false);
            _explanation.gameObject.SetActive(false);
            _hand.Clicked -= OnClicked;
        }

        public void Init(TutorialStepSO tutorialData, SaveLoader saveLoader)
        {
            _tutorialData = tutorialData;
            _saveLoader = saveLoader;
        }

        private void OnClicked()
        {
            _tutorialData.Completed();
            _saveLoader.Save();
            gameObject.SetActive(false);
        }
    }
}
