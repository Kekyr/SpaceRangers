using System;

public interface IChanging
{
    public event Action<float, float> ChangedValue;
}
