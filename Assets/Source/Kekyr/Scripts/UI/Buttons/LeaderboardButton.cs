using System;
using LeaderboardBase;
using UnityEngine;
using YG;

namespace UI
{
    public class LeaderboardButton
    {
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private AuthorizationPopup _authorizationPopup;

        private void OnEnable()
        {
            if (_leaderboard == null)
            {
                throw new ArgumentNullException(nameof(_leaderboard));
            }
        }

        private void OnClick()
        {
            if (YandexGame.auth == false)
            {
                _authorizationPopup.gameObject.SetActive(true);
                return;
            }

            _leaderboard.Fill();
        }
    }
}