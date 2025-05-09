using System;
using Enemy;
using LevelEnemy;
using UnityEngine;

public class WinHandler : MonoBehaviour
{
    private Timer _timer;
    private EnemyShip _boss;
    private LevelSO _levelData;

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

    public void Init(Timer timer, LevelSO levelData)
    {
        _timer = timer;
        _levelData = levelData;
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
            Won?.Invoke();
        }
    }

    private void OnDestroyed()
    {
        Won?.Invoke();
    }
}