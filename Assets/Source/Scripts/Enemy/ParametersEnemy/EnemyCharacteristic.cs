using System;
using UnityEngine;

public abstract class EnemyCharacteristic : MonoBehaviour
{
    public event Action<float, float> ChangedValue;

    protected void InvokeChangedValue(float value, float startValue)
    {
        ChangedValue?.Invoke(value, startValue);
    }
}
