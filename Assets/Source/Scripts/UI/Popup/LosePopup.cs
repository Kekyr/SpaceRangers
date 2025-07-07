using System;
using Audio;
using Game;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LosePopup : MonoBehaviour
    {
        [SerializeField] private Image _blackout;

        private GameEndHandler _gameEndHandler;
        private PopupTutorial _tutorial;

        private void Start()
        {
            if (_blackout == null)
            {
                throw new ArgumentNullException(nameof(_blackout));
            }
        }

        private void OnDestroy()
        {
            _gameEndHandler.Lose -= OnLose;
        }

        public void Init(GameEndHandler gameEndHandler, PopupTutorial tutorial)
        {
            _gameEndHandler = gameEndHandler;
            _tutorial = tutorial;

            _gameEndHandler.Lose += OnLose;
        }

        private void OnLose()
        {
            _blackout.gameObject.SetActive(true);
            gameObject.SetActive(true);
            _tutorial.enabled = true;
        }
    }
}