using System;
using ShipBase;
using UnityEngine;
using UnityEngine.UI;

public class LosePopup : MonoBehaviour
{
    [SerializeField] private Image _blackout;
    
    private ShipHealth _health;

    private void Awake()
    {
        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }
    }

    private void OnDestroy()
    {
        _health.Died -= OnDead;
    }

    public void Init(ShipHealth health)
    {
        _health = health;
        _health.Died += OnDead;
    }

    private void OnDead()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
}