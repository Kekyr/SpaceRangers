using System.Collections.Generic;
using UnityEngine;

namespace LevelEnemy
{
    [CreateAssetMenu(fileName = "new LevelSO", menuName = "LevelSO/Create new LevelSO", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private int _duration;
        [SerializeField] private List<EnemySpawnerSO> _spawnersData;

        public int Duration => _duration;
        public List<EnemySpawnerSO> SpawnersData => _spawnersData;
    }
}