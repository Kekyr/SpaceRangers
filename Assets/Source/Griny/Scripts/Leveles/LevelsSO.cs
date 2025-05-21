using System.Collections.Generic;
using UnityEngine;

namespace LevelEnemy
{
    [CreateAssetMenu(fileName = "new LevelsSO", menuName = "LevelsSO/Create new LevelsSO", order = 0)]
    public class LevelsSO : ScriptableObject
    {
        [SerializeField] private List<LevelSO> _data;
        [SerializeField] private int _currentIndex;

        public List<LevelSO> Data => _data;

        public int CurrentIndex => _currentIndex;
        public LevelSO Current => _data[_currentIndex];

        public LevelSO Next
        {
            get
            {
                int nextIndex=_currentIndex + 1;
                
                if(nextIndex < _data.Count)
                {
                    return _data[_currentIndex + 1];
                }

                return Current;
            }
        }

        public void SetCurrent(LevelSO levelData)
        {
            _currentIndex = _data.IndexOf(levelData);
        }
    }
}