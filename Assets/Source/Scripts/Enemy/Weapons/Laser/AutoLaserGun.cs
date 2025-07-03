using System;
using System.Collections;
using Enemy;
using UnityEngine;

public class AutoLaserGun : LaserGun
{
    [SerializeField] private float _startDelay;
    [SerializeField] private float _duration;
    [SerializeField] private float _interval;

    private Coroutine _shoot;
    private WaitForSeconds _waitStartDelay;
    private WaitForSeconds _waitDuration;
    private WaitForSeconds _waitInterval;

    protected override void Awake()
    {
        if (_duration == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_duration));
        }

        if (_interval == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_interval));
        }

        _waitStartDelay = new WaitForSeconds(_startDelay);
        _waitDuration = new WaitForSeconds(_duration);
        _waitInterval = new WaitForSeconds(_interval);

        base.Awake();
    }

    private IEnumerator Shoot()
    {
        yield return _waitStartDelay;

        while (gameObject.activeSelf == true)
        {
            Fire();
            yield return _waitDuration;
            Disable();
            yield return _waitInterval;
        }
    }

    protected override void OnEmptied()
    {
        _shoot = StartCoroutine(Shoot());
    }

    protected override void OnDied()
    {
        StopCoroutine(_shoot);
        base.OnDied();
    }
}