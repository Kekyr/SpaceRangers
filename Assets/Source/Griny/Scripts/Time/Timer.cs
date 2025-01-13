using System;
using System.Collections;
using UnityEngine;

namespace TimeEnemy
{
    public class Timer : MonoBehaviour
    {
        private const float _second = 1f;

        private WaitForSeconds _delyeCoroutine = new WaitForSeconds(_second);
        private int _minTime = 0;
        private int _timeLevel;
        private Coroutine _timerRunning;

        public event Action TimerStarted;
        public event Action<int> TimeChanged;
        public event Action TimerFinished;


        private void StartTimer(int seconds)
        {
            _timeLevel = seconds;

            if(_timerRunning != null)
            {
                StopCoroutine(_timerRunning);
            }

            _timerRunning = StartCoroutine(MoveTime());
            TimerStarted?.Invoke();
        }
        private IEnumerator MoveTime()
        {
            while(_timeLevel > _minTime)
            {
                _timeLevel--;
                TimeChanged?.Invoke(_timeLevel);
                yield return _delyeCoroutine;
            }

            TimerFinished?.Invoke();
        }
    }
}

