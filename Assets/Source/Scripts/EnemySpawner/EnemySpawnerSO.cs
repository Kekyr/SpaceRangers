using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new EnemySpawnerSO", menuName = "EnemySpawnerSO/Create new EnemySpawnerSO")]
public class EnemySpawnerSO : ScriptableObject
{
    [FormerlySerializedAs("_prefabs")] [SerializeField] private List<GameObject> _sequence;
    public List<GameObject> Sequence => _sequence;
}