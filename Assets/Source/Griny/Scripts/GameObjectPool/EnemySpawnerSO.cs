using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnemySpawnerSO", menuName = "EnemySpawnerSO/Create new EnemySpawnerSO")]
public class EnemySpawnerSO : ScriptableObject
{
    
    [SerializeField] private List<GameObject> _prefabs;

    public List<GameObject> Prefabs => _prefabs;
}