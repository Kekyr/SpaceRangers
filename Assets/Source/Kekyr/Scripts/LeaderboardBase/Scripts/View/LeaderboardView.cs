using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace LeaderboardBase
{
    public class LeaderboardView : MonoBehaviour
    {
        private const int Size = 5;

        [SerializeField] private Transform _container;
        [SerializeField] private Transform _playerScoreContainer;

        [SerializeField] private LeaderboardElement _leaderboardElementPrefab;
        [SerializeField] private LeaderboardElement _playerLeaderboardElementPrefab;

        [SerializeField] private Image _blackout;
        [SerializeField] private Button _closeButton;

        private List<LeaderboardElement> _spawnedElements = new();

        private void Start()
        {
            if (_container == null)
            {
                throw new ArgumentNullException(nameof(_container));
            }

            if (_playerScoreContainer == null)
            {
                throw new ArgumentNullException(nameof(_playerScoreContainer));
            }

            if (_leaderboardElementPrefab == null)
            {
                throw new ArgumentNullException(nameof(_leaderboardElementPrefab));
            }

            if (_blackout == null)
            {
                throw new ArgumentNullException(nameof(_blackout));
            }

            if (_closeButton == null)
            {
                throw new ArgumentNullException(nameof(_closeButton));
            }
            
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Close);
        }

        public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers)
        {
            ClearLeaderboard();

            string playerId = YandexGame.playerId;
            int length = leaderboardPlayers.Count < Size ? leaderboardPlayers.Count : Size;
            
            for (int i = 0; i < length; i++)
            {
                if (leaderboardPlayers[i].Id == playerId)
                {
                    SpawnLeaderboardElement(
                        _playerLeaderboardElementPrefab, 
                        leaderboardPlayers[i], 
                        _container);
                }
                else
                {
                    SpawnLeaderboardElement(_leaderboardElementPrefab, leaderboardPlayers[i], _container);
                }
            }

            for (int i = 0; i < leaderboardPlayers.Count; i++)
            {
                if (leaderboardPlayers[i].Id == playerId)
                {
                    SpawnLeaderboardElement(
                        _playerLeaderboardElementPrefab,
                        leaderboardPlayers[i],
                        _playerScoreContainer);
                }
            }

            _blackout.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        private void SpawnLeaderboardElement(LeaderboardElement prefab, LeaderboardPlayer player, Transform container)
        {
            LeaderboardElement leaderboardElementInstance = Instantiate(prefab, container);
            leaderboardElementInstance.Initialize(player.Avatar, player.Name, player.Rank, player.Score);
            _spawnedElements.Add(leaderboardElementInstance);
        }

        private void ClearLeaderboard()
        {
            foreach (var element in _spawnedElements)
            {
                Destroy(element.gameObject);
            }

            _spawnedElements = new List<LeaderboardElement>();
        }

        private void Close()
        {
            _blackout.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}