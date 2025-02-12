using System;
using UnityEngine;

public abstract class CharacteristicEnemy : MonoBehaviour
{
    public event Action<float, float> ChangedValue;

    protected void GetActionChangedValue(float value, float startValue)
    {
        ChangedValue?.Invoke(value, startValue);
    }
}
