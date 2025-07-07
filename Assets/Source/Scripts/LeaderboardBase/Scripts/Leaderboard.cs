using System.Collections.Generic;
using Game;
using Lean.Localization;
using ScoreSystem;
using UnityEngine;
using YG;
using YG.Utils.LB;

namespace LeaderboardBase
{
    public class Leaderboard : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";
        private const string TranslationName = "AnonymPhrase";
        private const string AnonymName = "anonymous";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        [SerializeField] private LeaderboardView _leaderboardView;

        private GameEndHandler _gameEndHandler;
        private LeanTranslation _translation;
        private ScoreSO _scoreData;

        private bool _isButtonClicked;

        private void OnEnable()
        {
            _translation = LeanLocalization.GetTranslation(TranslationName);

            if (_gameEndHandler != null)
            {
                _gameEndHandler.Won += SetScore;
            }

            YandexGame.onGetLeaderboard += OnGet;
        }

        private void OnDisable()
        {
            if (_gameEndHandler != null)
            {
                _gameEndHandler.Won -= SetScore;
            }

            YandexGame.onGetLeaderboard -= OnGet;
        }

        public void Init(GameEndHandler gameEndHandler, ScoreSO scoreData)
        {
            _gameEndHandler = gameEndHandler;
            _scoreData = scoreData;
            enabled = true;
        }

        public void Init(ScoreSO scoreData)
        {
            _scoreData = scoreData;
            enabled = true;
        }

        public void SetScore()
        {
            if (YandexGame.auth == false)
            {
                return;
            }

            YandexGame.GetLeaderboard(LeaderboardName, 10, 3, 3, "medium");
        }

        public void Fill()
        {
            if (YandexGame.auth == false)
            {
                return;
            }

            _leaderboardPlayers.Clear();
            _isButtonClicked = true;
            YandexGame.GetLeaderboard(LeaderboardName, 10, 3, 3, "medium");
        }

        private void OnGet(LBData lb)
        {
            if (lb.technoName != LeaderboardName)
            {
                return;
            }

            TryChangeScore(lb);

            if (_isButtonClicked == true)
            {
                CreateLeaderboard(lb);
                _isButtonClicked = false;
            }
        }

        private void CreateLeaderboard(LBData lb)
        {
            foreach (var playerData in lb.players)
            {
                string id = playerData.uniqueID;
                string avatar = playerData.photo;
                string name = playerData.name;

                if (string.IsNullOrEmpty(name) || name == AnonymName)
                {
                    name = (string)_translation.Data;
                }

                int rank = playerData.rank;
                int score = playerData.score;

                _leaderboardPlayers.Add(new LeaderboardPlayer(id, avatar, name, rank, score));
            }

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        }

        private void TryChangeScore(LBData lb)
        {
            if (lb.thisPlayer.score < _scoreData.Points)
            {
                YandexGame.NewLeaderboardScores(LeaderboardName, _scoreData.Points);
            }
        }
    }
}