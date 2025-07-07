using System;
using System.Collections;
using Level;
using UnityEngine;

namespace TimerSystem
{
    public class Timer : MonoBehaviour
    {
        private readonly int _interval = 1;

        private WaitForSeconds _wait;
        private LevelSO _levelData;

        private int _duration;

        public event Action<int, int> Changed;
        public event Action Ended;

        public int Duration => _duration;

        private void Start()
        {
            _duration = _levelData.Duration;
            _wait = new WaitForSeconds(_interval);

            Change();
            StartCoroutine(Count());
        }

        public void Init(LevelSO levelData)
        {
            _levelData = levelData;
            enabled = true;
        }

        private IEnumerator Count()
        {
            while (_duration != 0)
            {
                yield return _wait;
                _duration--;
                Change();
            }

            Ended?.Invoke();
        }

        private void Change()
        {
            int oneMinute = 60;

            int minutes = (_duration / oneMinute);
            int seconds = (_duration % oneMinute);

            Changed?.Invoke(minutes, seconds);
        }
    }
}