using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShipBase
{
    public abstract class ImprovementsSO<T> : ScriptableObject
    {
        [SerializeField] private ImprovementSO<T>[] _levels;
        [SerializeField] private int _currentLevelIndex;

        public T CurrentLevel => _levels[_currentLevelIndex].Value;

        public ImprovementDataSO[] Levels => _levels;

        public void Init(int currentLevelIndex)
        {
            _currentLevelIndex = currentLevelIndex;
        }

        public void SetCurrent(ImprovementDataSO data)
        {
            List<ImprovementSO<T>> list = _levels.ToList();
            ImprovementSO<T> fullData = (ImprovementSO<T>)data;
            _currentLevelIndex = list.IndexOf(fullData);
        }
    }
}