using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyDataSO", menuName = "EnemyDataSO/Create new EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private int _pointsCount;
    [SerializeField] private int[] _coinsCount;

    public int PointsCount => _pointsCount;
    public int[] CoinsCount => _coinsCount;
}