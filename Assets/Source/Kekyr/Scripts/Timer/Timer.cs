using System;
using System.Collections;
using LevelEnemy;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private readonly int _endTime = 10;
    private readonly int _interval = 1;
    
    private WaitForSeconds _wait;
    private LevelSO _levelData;
    
    private int _duration;
    
    public event Action<int,int> Changed;
    public event Action Ends;
    public event Action Ended;
    public event Action Won;

    public int EndTime => _endTime;
    public int Duration => _duration;

    private void Start()
    {
        _duration = _levelData.Duration;
        
        _wait = new WaitForSeconds(_interval);
        
        Change();
        StartCoroutine(Count());
    }

    public void Init(LevelSO levelData)
    {
        _levelData = levelData;
        enabled = true;
    }

    private IEnumerator Count()
    {
        while (_duration != 0)
        {
            yield return _wait;
            _duration--;
            
            Change();
            
            if (_duration <= _endTime)
            {
                Ends?.Invoke();
            }
        }
        
        Ended?.Invoke();

        if (_levelData.HasBoss == false)
        {
            Won?.Invoke();
        }
    }

    private void Change()
    {
        int oneMinute = 60;

        int minutes = (_duration / oneMinute);
        int seconds = (_duration % oneMinute);
        
        Changed?.Invoke(minutes,seconds);
    }
}