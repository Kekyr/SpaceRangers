using System;
using Enemy;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _points;

    public event Action<int> Changed;
    
    public void Add(EnemyShip enemy)
    {
        if (enemy.Data.PointsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(enemy.Data.PointsCount));
        }

        _points += enemy.Data.PointsCount;
        Changed?.Invoke(_points);
    }
}