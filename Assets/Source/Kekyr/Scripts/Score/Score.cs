using System;
using Enemy;
using UnityEngine;

public class Score : MonoBehaviour
{
    private ScoreSO _data;
    private GameEndHandler _gameEndHandler;
    private SaveLoader _saveLoader;
    
    private int _points;

    public event Action<int> Changed;

    private void Start()
    {
        _gameEndHandler.Won += OnWon;
    }

    private void OnDestroy()
    {
        _gameEndHandler.Won -= OnWon;
    }

    public void Init(GameEndHandler gameEndHandler, ScoreSO data, SaveLoader saveLoader)
    {
        _gameEndHandler = gameEndHandler;
        _data = data;
        _saveLoader = saveLoader;
        enabled = true;
    }
    
    public void Add(EnemyShip enemy)
    {
        if (enemy.Data.PointsCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(enemy.Data.PointsCount));
        }

        _points += enemy.Data.PointsCount;
        Changed?.Invoke(_points);
    }

    private void OnWon()
    {
        _data.Add(_points);
        _saveLoader.Save();
    }
}