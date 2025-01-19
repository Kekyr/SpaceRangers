using UnityEngine;

public abstract class Attacker: MonoBehaviour
{
    [SerializeField] private int _damage;

    public int Damage => _damage;
}