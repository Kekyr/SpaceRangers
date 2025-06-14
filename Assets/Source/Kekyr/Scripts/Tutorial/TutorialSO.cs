using UnityEngine;

[CreateAssetMenu(fileName = "new TutorialSO", menuName = "TutorialSO/Create new TutorialSO")]
public class TutorialSO : ScriptableObject
{
    [SerializeField] private TutorialStepSO[] _steps;
    [SerializeField] private int _currentIndex;
    [SerializeField] private bool _isFinished;

    public TutorialStepSO Current => _steps[_currentIndex];
    public int CurrentIndex => _currentIndex;
    public bool IsFinished => _isFinished;

    public void Init(bool[] stepsState, int currentIndex, bool isFinished)
    {
        for (int i = 0; i < _steps.Length; i++)
        {
            _steps[i].Init(stepsState[i]);
        }

        _currentIndex = currentIndex;
        _isFinished = isFinished;
    }

    public void Completed()
    {
        _steps[_currentIndex].Completed();

        if (_currentIndex + 1 >= _steps.Length)
        {
            _isFinished = true;
            return;
        }

        _currentIndex++;
    }

    public void Reset()
    {
        for (int i = 0; i < _steps.Length; i++)
        {
            _steps[i].Reset();
        }

        _currentIndex = 0;
        _isFinished = false;
    }
}