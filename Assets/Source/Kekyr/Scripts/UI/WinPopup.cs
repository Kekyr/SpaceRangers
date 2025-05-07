using System;
using Audio;
using Enemy;
using UnityEngine;

[RequireComponent(typeof(SFX))]
public class WinPopup : MonoBehaviour
{
    [SerializeField] private SFXSO _winSfx;

    private Timer _timer;
    private EnemyShip _boss;
    private SFX _sfx;

    private void Awake()
    {
        if (_winSfx == null)
        {
            throw new ArgumentNullException(nameof(_winSfx));
        }

        _sfx = GetComponent<SFX>();
    }

    private void OnDisable()
    {
        _timer.Won -= OnWon;
    }

    public void Init(Timer timer)
    {
        _timer = timer;
        _timer.Won += OnWon;
    }

    public void Init(EnemyShip boss)
    {
        _boss = boss;
        _boss.Destroyed += OnWon;
    }

    private void OnWon()
    {
        gameObject.SetActive(true);
    }

    private void OnAnimationStarted()
    {
        _sfx.Play(_winSfx);
    }
}