using System;
using LeaderboardBase;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class AuthorizationPopup : MonoBehaviour
    {
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Image _blackout;

        private Leaderboard _leaderboard;

        private void OnEnable()
        {
            if (_signInButton == null)
            {
                throw new ArgumentNullException(nameof(_signInButton));
            }

            if (_closeButton == null)
            {
                throw new ArgumentNullException(nameof(_closeButton));
            }

            if (_blackout == null)
            {
                throw new ArgumentNullException(nameof(_blackout));
            }

            _signInButton.onClick.AddListener(SignIn);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _signInButton.onClick.RemoveListener(SignIn);
            _closeButton.onClick.RemoveListener(Close);
        }

        public void Init(Leaderboard leaderboard)
        {
            _leaderboard = leaderboard;
            enabled = true;
        }

        private void SignIn()
        {
            if (YandexGame.auth == false)
            {
                YandexGame.AuthDialog();
            }
            else
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
                _leaderboard.Fill();
            }
        }

        private void Close()
        {
            _blackout.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}