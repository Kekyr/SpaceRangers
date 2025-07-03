using System;
using Enemy;
using UnityEngine;

public class Score : MonoBehaviour
{
    private ScoreSO _data;
    private GameEndHandler _gameEndHandler;
    private SaveLoader _saveLoader;
    private RewardedAd _rewardedAd;
    
    private int _points;

    public event Action<int> Changed;

    public int Points => _points;

    private void Start()
    {
        _gameEndHandler.Won += OnWon;
        _rewardedAd.Rewarded += OnRewarded;
    }

    private void OnDestroy()
    {
        _gameEndHandler.Won -= OnWon;
        _rewardedAd.Rewarded -= OnRewarded;
    }

    public void Init(GameEndHandler gameEndHandler, ScoreSO data, SaveLoader saveLoader, RewardedAd rewardedAd)
    {
        _gameEndHandler = gameEndHandler;
        _data = data;
        _saveLoader = saveLoader;
        _rewardedAd = rewardedAd;
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

    private void OnRewarded()
    {
        int multiplier = 2;
        
        _data.Add(_points);
        _points *= multiplier;
        _saveLoader.Save();
    }
}