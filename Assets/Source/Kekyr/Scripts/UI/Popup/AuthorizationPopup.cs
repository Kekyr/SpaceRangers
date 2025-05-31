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

        private Leaderboard _leaderboard;

        private void OnEnable()
        {
            if (_signInButton == null)
            {
                throw new ArgumentNullException(nameof(_signInButton));
            }

            _signInButton.onClick.AddListener(SignIn);
        }

        private void OnDisable()
        {
            _signInButton.onClick.RemoveListener(SignIn);
        }

        public void Init(Leaderboard leaderboard)
        {
            _leaderboard = leaderboard;
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
    }
}