using System.Collections.Generic;
using UnityEngine;
using YG;

public interface IImprovementsSO
{
    public ImprovementDataSO[] Levels { get; }

    public int CurrentIndex { get; }

    public void SetCurrent(ImprovementDataSO data);

    public void Init(int currentLevelIndex, List<ImprovementState> improvementStates);

    public void Reset();
    
}