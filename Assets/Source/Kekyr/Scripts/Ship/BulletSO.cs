using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new BulletSO", menuName = "BulletSO/Create new BulletSO")]
public class BulletSO : ScriptableObject
{
    [SerializeField] private GameObject[] _prefabs;

    [SerializeField] private int _currentPrefabIndex;

    public GameObject CurrentPrefab => _prefabs[_currentPrefabIndex];

    public IReadOnlyCollection<GameObject> Prefabs => _prefabs;

    public void Init(int currentPrefabIndex)
    {
        _currentPrefabIndex = currentPrefabIndex;
    }

    public void Improve()
    {
        int nextPrefabIndex = _currentPrefabIndex + 1;

        if (nextPrefabIndex >= _prefabs.Length)
        {
            return;
        }

        _currentPrefabIndex++;
    }
}