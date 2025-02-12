using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class ValueBar : MonoBehaviour
    {
        [SerializeReference] private CharacteristicEnemy _characteristic;
        [SerializeField] public Slider _slider;
        [SerializeField] private float _speedChange;

        private float _targetHealth;
        private Coroutine _coroutine;
        private float _maxValue = 1;


        private void Awake()
        {
            _slider.value = _maxValue;
        }

        private void OnEnable()
        {
            _characteristic.ChangedValue += OnChangeValue;
        }

        private void OnDisable()
        {
            _characteristic.ChangedValue -= OnChangeValue;
        }

        private void OnChangeValue(float value, float startValue)
        {
            _targetHealth = value / startValue;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ChangHelth(_targetHealth));
        }

        private IEnumerator ChangHelth(float targetHealth)
        {
            while (targetHealth != _slider.value)
            {
                ChangHealthSlow(targetHealth);
                yield return null;
            }
        }

        private void ChangHealthSlow(float targetHealth)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetHealth, _speedChange * Time.deltaTime);
        }
    }
}