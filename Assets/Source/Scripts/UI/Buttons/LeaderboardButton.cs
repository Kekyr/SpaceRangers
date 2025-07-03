using System;
using LeaderboardBase;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    public class LeaderboardButton : MonoBehaviour
    {
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private AuthorizationPopup _authorizationPopup;
        [SerializeField] private Image _blackout;

        private Button _button;

        private void Start()
        {
            if (_leaderboard == null)
            {
                throw new ArgumentNullException(nameof(_leaderboard));
            }

            if (_authorizationPopup == null)
            {
                throw new ArgumentNullException(nameof(_authorizationPopup));
            }

            if (_blackout == null)
            {
                throw new ArgumentNullException(nameof(_blackout));
            }

            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
        
        private void OnClick()
        {
            if (YandexGame.auth == false)
            {
                _blackout.gameObject.SetActive(true);
                _authorizationPopup.gameObject.SetActive(true);
                return;
            }

            _leaderboard.Fill();
        }
    }
}