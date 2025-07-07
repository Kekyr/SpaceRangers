using TMPro;
using UnityEngine;

namespace TimerSystem
{
    public class TimerView : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;
        private Timer _timer;

        private void OnEnable()
        {
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            _timer.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _timer.Changed -= OnChanged;
        }

        public void Init(Timer timer)
        {
            _timer = timer;
            enabled = true;
        }

        private void OnChanged(int minutes, int seconds)
        {
            string newTime = "0";
            int minSeconds = 10;

            newTime = seconds >= minSeconds
                ? newTime + minutes + ":" + seconds
                : newTime + minutes + ":" + "0" + seconds;

            _textMeshPro.text = newTime;
        }
    }
}