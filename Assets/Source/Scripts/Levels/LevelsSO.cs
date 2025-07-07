using System.Collections.Generic;
using UnityEngine;
using YG;

namespace LevelEnemy
{
    [CreateAssetMenu(fileName = "new LevelsSO", menuName = "LevelsSO/Create new LevelsSO", order = 0)]
    public class LevelsSO : ScriptableObject
    {
        [SerializeField] private List<LevelSO> _data;
        [SerializeField] private int _currentIndex;
        private bool _isFirstTime = true;

        public List<LevelSO> Data => _data;
        public int CurrentIndex => _currentIndex;
        public bool IsFirstTime => _isFirstTime;
        public LevelSO Current => _data[_currentIndex];

        public LevelSO Next
        {
            get
            {
                int nextIndex = _currentIndex + 1;

                if (nextIndex < _data.Count)
                {
                    return _data[_currentIndex + 1];
                }

                return Current;
            }
        }

        public void Init(List<LevelState> levelStates, int currentIndex)
        {
            if (levelStates!=null && levelStates.Count!=0)
            {
                for (int i = 0; i < _data.Count; i++)
                {
                    _data[i].Init(levelStates[i]);
                }
            }

            _currentIndex = currentIndex;
            _data[_currentIndex].Choose();
        }

        public void SetCurrent(LevelSO levelData)
        {
            _data[_currentIndex].UnChoose();
            _currentIndex = _data.IndexOf(levelData);
            _data[_currentIndex].Choose();
        }

        public void Reset()
        {
            for (int i = 0; i < _data.Count; i++)
            {
                _data[i].Reset();
            }

            _data[0].Opened();

            _currentIndex = 0;
        }

        public void Played()
        {
            _isFirstTime = false;
        }
    }
}