using System;
using UnityEngine;

public class DirectionChanger : MonoBehaviour
{
    private Vector2 _current;

    public event Action OutSight;
    public event Action<Vector2> DirectionChanged;

    private void Start()
    {
        _current = Vector2.down;
        DirectionChanged?.Invoke(_current);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("BorderDown"))
        {
            gameObject.SetActive(false);
            OutSight?.Invoke();
        }
    }
}