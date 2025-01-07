using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ShipBase
{
    public class HealthView : MonoBehaviour
    {
        private readonly float _duration = 0.5f;
        
        private Slider _slider;
        private Health _health;

        private void OnEnable()
        {
            _slider = GetComponent<Slider>();

            _slider.maxValue = _health.Max;
            _slider.value = _health.Max;

            _health.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _health.ValueChanged -= OnValueChanged;
        }

        public void Init(Health health)
        {
            _health = health;
            enabled = true;
        }

        private void OnValueChanged(float value)
        {
            _slider.DOValue(value, _duration);
        }
    }
}