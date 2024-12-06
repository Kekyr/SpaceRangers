using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _count;

    private Queue<GameObject> _instances = new Queue<GameObject>();

    private void OnEnable()
    {
        if (_prefab == null)
        {
            throw new ArgumentNullException();
        }

        if (_count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_count));
        }
        
        for (int i = 0; i < _count; i++)
        {
            GameObject instance = Instantiate(_prefab, transform);
            instance.SetActive(false);
            _instances.Enqueue(instance);
        }
    }

    public GameObject Spawn(Vector3 position)
    {
        GameObject instance = _instances.Dequeue();

        instance.gameObject.SetActive(true);
        instance.transform.position = position;

        _instances.Enqueue(instance);

        return instance;
    }
}