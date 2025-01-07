using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ShipBase
{
    public class ShieldView : MonoBehaviour
    {
        private readonly float _duration = 0.5f;

        private Slider _slider;
        private Shield _shield;

        private void OnEnable()
        {
            _slider = GetComponent<Slider>();

            _slider.maxValue = _shield.Max;
            _slider.value = _shield.Max;

            _shield.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _shield.ValueChanged -= OnValueChanged;
        }

        public void Init(Shield shield)
        {
            _shield = shield;
            enabled = true;
        }

        private void OnValueChanged(float value)
        {
            _slider.DOValue(value, _duration);
        }
    }
}