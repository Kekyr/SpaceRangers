using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyDataSO", menuName = "EnemyDataSO/Create new EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private int[] _coinsCount;

    public int[] CoinsCount => _coinsCount;
}