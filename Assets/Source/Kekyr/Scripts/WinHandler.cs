using System;
using System.Collections;
using Enemy;
using LevelEnemy;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    private readonly float _delay = 2f;
    
    private Timer _timer;
    private EnemyShip _boss;
    private LevelSO _levelData;
    private LevelsSO _levelsData;

    private WaitForSeconds _wait;

    public event Action Won;

    private void OnEnable()
    {
        _wait = new WaitForSeconds(_delay);
        
        _timer.Ended += OnEnded;
    }

    private void OnDisable()
    {
        _timer.Ended -= OnEnded;

        if (_boss != null)
        {
            _boss.Destroyed -= OnDestroyed;
        }
    }

    public void Init(Timer timer, LevelSO levelData, LevelsSO levelsData)
    {
        _timer = timer;
        _levelData = levelData;
        _levelsData = levelsData;
        enabled = true;
    }

    public void Init(EnemyShip boss)
    {
        _boss = boss;
        _boss.Destroyed += OnDestroyed;
    }

    private void OnEnded()
    {
        if (_levelData.HasBoss == false)
        {
            StartCoroutine(OnWon());
        }
    }

    private void OnDestroyed()
    {
        StartCoroutine(OnWon());
    }

    private IEnumerator OnWon()
    {
        yield return _wait;

        _levelData.Completed();
        _levelsData.Next.Opened();
        Won?.Invoke();
    }
}