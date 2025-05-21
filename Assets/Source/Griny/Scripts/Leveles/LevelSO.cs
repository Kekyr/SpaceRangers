using System.Collections.Generic;
using UnityEngine;

namespace LevelEnemy
{
    [CreateAssetMenu(fileName = "new LevelSO", menuName = "LevelSO/Create new LevelSO", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private int _duration;
        [SerializeField] private List<EnemySpawnerSO> _spawnersData;
        [SerializeField] private bool _hasBoss;
        [SerializeField] private GameObject _bossPrefab;
        [SerializeField] private bool _isOpened;
        [SerializeField] private bool _isCompleted;

        public int Duration => _duration;
        public List<EnemySpawnerSO> SpawnersData => _spawnersData;
        public bool HasBoss => _hasBoss;
        public GameObject BossPrefab => _bossPrefab;
        public bool IsOpened => _isOpened;
        public bool IsCompleted => _isCompleted;

        public void Opened()
        {
            _isOpened = true;
        }

        public void Completed()
        {
            _isCompleted = true;
        }
    }
}