using System;
using UnityEngine;

namespace Enemy
{
    public abstract class CharacteristicEnemy : MonoBehaviour
    {
        public event Action<float, float> ChangedValue;

        protected void InvokeChangedValue(float value, float startValue)
        {
            ChangedValue?.Invoke(value, startValue);
        }
    }
}
