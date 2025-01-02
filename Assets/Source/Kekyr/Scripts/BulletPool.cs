using System;
using System.Collections.Generic;
using ShipBase;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _count;
    [SerializeField] private Health _health;

    private GameObject _prefab;

    private Queue<GameObject> _instances = new Queue<GameObject>();

    private void OnEnable()
    {
        if (_count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_count));
        }

        if (_health == null)
        {
            throw new ArgumentNullException(nameof(_health));
        }

        _health.Died += OnDead;

        for (int i = 0; i < _count; i++)
        {
            GameObject instance = Instantiate(_prefab, transform);
            instance.SetActive(false);
            _instances.Enqueue(instance);
        }
    }

    private void OnDisable()
    {
        _health.Died -= OnDead;
    }

    public void Init(GameObject prefab)
    {
        _prefab = prefab;
        enabled = true;
    }

    public GameObject Spawn(Vector3 position)
    {
        GameObject instance = _instances.Dequeue();

        instance.gameObject.SetActive(true);
        instance.transform.position = position;

        _instances.Enqueue(instance);

        return instance;
    }

    private void OnDead()
    {
        gameObject.SetActive(false);
    }
}