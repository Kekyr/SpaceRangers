using UnityEngine;

public interface IImprovementsSO
{
    public ImprovementDataSO[] Levels { get; }
    
    public void SetCurrent(ImprovementDataSO data);

    public void Reset();
    
}