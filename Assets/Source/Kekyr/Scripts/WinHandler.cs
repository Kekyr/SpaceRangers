using System;
using Enemy;
using LevelEnemy;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    private Timer _timer;
    private EnemyShip _boss;
    private LevelSO _levelData;
    private LevelsSO _levelsData;

    public event Action Won;

    private void OnEnable()
    {
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
            OnWon();
        }
    }

    private void OnDestroyed()
    {
        OnWon();
    }

    private void OnWon()
    {
        Won?.Invoke();
        _levelData.Completed();
        _levelsData.Next.Opened();
    }
}