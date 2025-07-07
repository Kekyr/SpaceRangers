using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    [RequireComponent(typeof(Slider))]
    public class ValueBar : MonoBehaviour
    {
        [SerializeReference] private EnemyCharacteristic _characteristic;
        [SerializeField] private float _speedChange;

        private Slider _slider;
        private float _targetHealth;
        private Coroutine _coroutine;
        private float _maxValue = 1;
        
        private void Awake()
        {
            if (_characteristic == null)
            {
                throw new ArgumentNullException(nameof(_characteristic));
            }
            
            _slider = GetComponent<Slider>();
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

            _coroutine = StartCoroutine(ChangHealth(_targetHealth));
        }

        private IEnumerator ChangHealth(float targetHealth)
        {
            while (targetHealth != _slider.value)
            {
                ChangeHealthSlow(targetHealth);
                yield return null;
            }
        }

        private void ChangeHealthSlow(float targetHealth)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, targetHealth, _speedChange * Time.deltaTime);
        }
    }
}