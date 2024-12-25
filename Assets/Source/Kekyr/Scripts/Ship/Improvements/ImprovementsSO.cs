using System.Collections.Generic;
using UnityEngine;

namespace ShipBase
{
    public abstract class ImprovementsSO<T> : ScriptableObject
    {
        [SerializeField] private T[] _levels;
        [SerializeField] private int _currentLevelIndex;

        public T CurrentLevel => _levels[_currentLevelIndex];

        public IReadOnlyCollection<T> Levels => _levels;

        public void Init(int currentLevelIndex)
        {
            _currentLevelIndex = currentLevelIndex;
        }

        public void Improve()
        {
            int nextLevelIndex = _currentLevelIndex + 1;

            if (nextLevelIndex >= _levels.Length)
            {
                return;
            }

            _currentLevelIndex++;
        }
    }
}