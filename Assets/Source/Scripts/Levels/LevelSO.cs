using System.Collections.Generic;
using Enemy;
using UnityEngine;
using YG;

namespace Level
{
    [CreateAssetMenu(fileName = "new LevelSO", menuName = "LevelSO/Create new LevelSO", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private int _duration;
        [SerializeField] private List<EnemySpawnerSO> _spawnersData;
        [SerializeField] private bool _hasBoss;
        [SerializeField] private GameObject _bossPrefab;
        [SerializeField] private LevelState _state;
        [SerializeField] private bool _isCurrent;

        public int Duration => _duration;
        public List<EnemySpawnerSO> SpawnersData => _spawnersData;
        public bool HasBoss => _hasBoss;
        public GameObject BossPrefab => _bossPrefab;
        public LevelState Status => _state;
        public bool IsCurrent => _isCurrent;

        public void Init(LevelState state)
        {
            _state = state;
        }
        
        public void Opened()
        {
            if (_state != LevelState.Completed)
            {
                _state = LevelState.Opened;
            }
        }

        public void Completed()
        {
            _state = LevelState.Completed;
        }

        public void Choose()
        {
            _isCurrent = true;
        }

        public void UnChoose()
        {
            _isCurrent = false;
        }

        public void Reset()
        {
            _state = LevelState.Closed;
            _isCurrent = false;
        }
    }
}