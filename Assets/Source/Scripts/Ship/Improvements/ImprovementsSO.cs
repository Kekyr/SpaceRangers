using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

namespace ShipBase
{
    public abstract class ImprovementsSO<T> : ScriptableObject, IImprovementsSO
    {
        [SerializeField] private ImprovementSO<T>[] _levels;
        [SerializeField] private int _currentLevelIndex;

        public T CurrentLevel => _levels[_currentLevelIndex].Value;

        public ImprovementDataSO[] Levels => _levels;
        public int CurrentIndex => _currentLevelIndex;

        public void Init(List<ImprovementState> improvementStates, int currentLevelIndex)
        {
            if (improvementStates!=null && improvementStates.Count != 0  )
            {
                for (int i = 0; i < Levels.Length; i++)
                {
                    Levels[i].Init(improvementStates[i]);
                }
            }

            _currentLevelIndex = currentLevelIndex;
            Levels[_currentLevelIndex].Choose();
        }

        public void SetCurrent(ImprovementDataSO data)
        {
            List<ImprovementSO<T>> list = _levels.ToList();
            ImprovementSO<T> fullData = (ImprovementSO<T>)data;

            list[_currentLevelIndex].UnChoose();

            _currentLevelIndex = list.IndexOf(fullData);

            data.Buy();
            data.Choose();

            int nextLevelIndex = _currentLevelIndex + 1;

            if (nextLevelIndex >= _levels.Length)
            {
                return;
            }

            list[nextLevelIndex].Open();
        }

        public void Reset()
        {
            int firstElementIndex = 0;
            int secondElementIndex = 1;
            
            _currentLevelIndex = firstElementIndex;
            Levels[firstElementIndex].Choose();

            for (int i = 1; i < _levels.Length; i++)
            {
                Levels[i].Reset();
            }

            Levels[secondElementIndex].Open();
        }
    }
}