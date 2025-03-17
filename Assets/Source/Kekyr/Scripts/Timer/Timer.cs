using System;
using System.Collections;
using Audio;
using UnityEngine;

[RequireComponent(typeof(SFX))]
public class Timer : MonoBehaviour
{
    private readonly int _endTime = 10;
    private readonly int _interval = 1;

    [SerializeField] private SFXSO _winSFX;
    
    private WaitForSeconds _wait;
    private SFX _sfx;
    
    private int _duration;
    
    public event Action<int> Changed;
    public event Action Ends;
    public event Action Ended;

    public int EndTime => _endTime;

    private void OnEnable()
    {
        if (_winSFX == null)
        {
            throw new ArgumentNullException(nameof(_winSFX));
        }

        _sfx = GetComponent<SFX>();
        _wait = new WaitForSeconds(_interval);
        
        Changed?.Invoke(_duration);
        StartCoroutine(Count());
    }

    public void Init(int duration)
    {
        _duration = duration;
        enabled = true;
    }

    private IEnumerator Count()
    {
        while (_duration != 0)
        {
            yield return _wait;
            _duration--;

            Changed?.Invoke(_duration);
            
            if (_duration <= _endTime)
            {
                Ends?.Invoke();
            }
        }
        
        Ended?.Invoke();
        _sfx.Play(_winSFX);
    }
}