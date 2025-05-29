using System;
using System.Collections;
using Audio;
using Enemy;
using LevelEnemy;
using ShipBase;
using UnityEngine;

[RequireComponent(typeof(SFX))]
public class GameEndHandler : MonoBehaviour
{
    private readonly float _delay = 2f;

    [SerializeField] private SFXSO _winSfx;
    [SerializeField] private SFXSO _loseSfx;
    
    private Timer _timer;
    private EnemyShip _boss;
    private LevelSO _levelData;
    private LevelsSO _levelsData;
    private ShipHealth _shipHealth;
    private SFX _sfx;
    private AudioSettingSO _setting;

    private WaitForSeconds _wait;

    private bool _isEnded;

    public event Action Won;
    public event Action Lose;

    private void OnEnable()
    {
        if (_winSfx == null)
        {
            throw new ArgumentNullException(nameof(_winSfx));
        }

        if (_loseSfx == null)
        {
            throw new ArgumentNullException(nameof(_loseSfx));
        }
        
        _wait = new WaitForSeconds(_delay);
        _sfx = GetComponent<SFX>();
        _sfx.Init(_setting);

        _timer.Ended += OnEnded;
        _shipHealth.Died += OnDied;
    }

    private void OnDisable()
    {
        _timer.Ended -= OnEnded;
        _shipHealth.Died -= OnDied;

        if (_boss != null)
        {
            _boss.Destroyed -= OnDestroyed;
        }
    }

    public void Init(Timer timer, ShipHealth shipHealth, AudioSettingSO setting,LevelSO levelData, LevelsSO levelsData)
    {
        _timer = timer;
        _shipHealth = shipHealth;
        _setting = setting;
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

    private void OnDied()
    {
        if (_isEnded == false)
        {
            _isEnded = true;
            _sfx.Play(_loseSfx);
            Lose?.Invoke();
        }
    }

    private IEnumerator OnWon()
    {
        if (_isEnded == false)
        {
            _isEnded = true;
            yield return _wait;
            _sfx.Play(_winSfx);
            _levelData.Completed();
            _levelsData.Next.Opened();
            Won?.Invoke();
        }
    }
}