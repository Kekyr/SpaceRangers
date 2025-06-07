using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new TutorialStepSO", menuName = "TutorialStepSO/Create new TutorialStepSO")]
public class TutorialStepSO : ScriptableObject
{
    [SerializeField] private bool _isCompleted;
    [SerializeField] private string _animationTrigger;

    public bool IsCompleted => _isCompleted;
    public string AnimationTrigger => _animationTrigger;

    public void Init(bool isCompleted)
    {
        _isCompleted = isCompleted;
    }
    
    public void Completed()
    {
        _isCompleted = true;
    }
    
    public void Reset()
    {
        _isCompleted = false;
    }
}
