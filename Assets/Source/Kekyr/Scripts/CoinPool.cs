using System;
using System.Collections.Generic;
using Game;
using ShipBase;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinPool : MonoBehaviour
{
    private readonly float MinRandomX = -0.4f;
    private readonly float MaxRandomX = 0.4f;

    private readonly float MinRandomY = -0.4f;
    private readonly float MaxRandomY = 0.4f;

    [SerializeField] private Coin[] _prefabs;
    [SerializeField] private int _count;

    private Transform _destination;
    private ShipHealth _shipHealth;

    private List<Queue<Coin>> _instances = new List<Queue<Coin>>();

    private void OnEnable()
    {
        if (_prefabs.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_prefabs));
        }

        if (_count == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_count));
        }

        for (int i = 0; i < _prefabs.Length; i++)
        {
            GameObject container = new GameObject(_prefabs[i].name);
            container.transform.parent = transform;
            _instances.Add(new Queue<Coin>());

            for (int j = 0; j < _count; j++)
            {
                Coin instance = Instantiate(_prefabs[i], container.transform);
                instance.gameObject.SetActive(false);
                instance.GetComponent<CoinMovement>().Init(_destination);
                _instances[i].Enqueue(instance);
            }
        }

        _shipHealth.Died += OnDead;
    }

    private void OnDisable()
    {
        _shipHealth.Died -= OnDead;
    }

    public void Init(Transform destination, ShipHealth shipHealth)
    {
        _destination = destination;
        _shipHealth = shipHealth;
        enabled = true;
    }

    public void Spawn(Vector3 position, EnemyDataSO enemyData)
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            for (int j = 0; j < enemyData.CoinsCount[i]; j++)
            {
                Coin instance = _instances[i].Dequeue();
                Vector3 offset = new Vector3(Random.Range(MinRandomX, MaxRandomX),
                    Random.Range(MinRandomY, MaxRandomY));
                instance.transform.position = position + offset;
                instance.gameObject.SetActive(true);
                _instances[i].Enqueue(instance);
            }
        }
    }

    private void OnDead()
    {
        gameObject.SetActive(false);
    }
}