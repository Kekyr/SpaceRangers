using ShipBase;
using UnityEngine;

public class LosePopup : MonoBehaviour
{
    private ShipHealth _health;

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
        gameObject.SetActive(true);
    }
}