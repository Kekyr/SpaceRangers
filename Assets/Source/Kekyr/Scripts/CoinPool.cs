using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private Coin _prefab;
    [SerializeField] private int _count;

    private Transform _destination;

    private Queue<Coin> _instances = new Queue<Coin>();

    private void OnEnable()
    {
        if (_prefab == null)
        {
            throw new ArgumentNullException(nameof(_prefab));
        }
        
        if (_count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_count));
        }

        for (int i = 0; i < _count; i++)
        {
            Coin instance = Instantiate(_prefab, transform);
            instance.gameObject.SetActive(false);
            instance.GetComponent<CoinMovement>().Init(_destination);
            _instances.Enqueue(instance);
        }
    }

    public void Init(Transform destination)
    {
        _destination = destination;
        enabled = true;
    }

    public void Spawn(Vector3 position)
    {
        Coin instance = _instances.Dequeue();

        instance.gameObject.SetActive(true);
        instance.transform.position = position;

        _instances.Enqueue(instance);
    }
}