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

        base.Awake();
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_startDelay);
        
        while (gameObject.activeSelf == true)
        {
            Fire();
            yield return new WaitForSeconds(_duration);
            Disable();
            yield return new WaitForSeconds(_interval);
        }
    }

    protected override void OnEmptied()
    {
        _shoot = StartCoroutine(Shoot());
    }

    protected override void OnDestroyed()
    {
        StopCoroutine(_shoot);
        base.OnDestroyed();
    }
}