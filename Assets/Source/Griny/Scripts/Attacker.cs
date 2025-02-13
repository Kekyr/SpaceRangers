using System;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Attacker: MonoBehaviour
{
    [SerializeField] private Color _damageColor;
    [SerializeField] private int _damage;

    public int Damage => _damage;
    public Color DamageColor => _damageColor;

    private void OnEnable()
    {
        if (_damage == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_damage));
        }
    }
}