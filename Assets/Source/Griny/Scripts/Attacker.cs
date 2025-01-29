using System;
using UnityEngine;

public abstract class Attacker: MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage => _damage;

    private void OnEnable()
    {
        if (_damage == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_damage));
        }
    }
}