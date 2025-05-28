using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShipBase
{
    public abstract class ImprovementsSO<T> : ScriptableObject, IImprovementsSO
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
            Levels[0].Choose();

            for (int i = 1; i < _levels.Length; i++)
            {
                Levels[i].Reset();
            }
            
            Levels[1].Open();
        }
    }
}