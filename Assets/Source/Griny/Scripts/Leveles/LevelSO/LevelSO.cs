using UnityEngine;

namespace LevelEnemy
{
    [CreateAssetMenu(fileName = "Level 1", menuName = "LevelSO/Level", order = 0)]
    public class LevelSO : ScriptableObject
    {
        [SerializeField] private int _levelTime;

        public int LevelTime => _levelTime;
    }
}

