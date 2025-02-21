using Enemy;
using System.Collections;
using UnityEngine;
using WordGame;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : Attacker
{
    [SerializeField] private int _speed;

    private Transform _pointRocketOnShip;
    private Coroutine _coroutine;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _pointRocketOnShip = transform.parent;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void RunRocket()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(FlyRocket());
    }

    private IEnumerator FlyRocket()
    {
        gameObject.transform.parent = null;

        while (_speed != 0)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out BackgroundBorder border))
        {
            if (border.GetName() == "down")
            {
                _speed = 0;
                gameObject.SetActive(false);
                StopCoroutine(_coroutine);
            }
        }
    }

    public void Restart()
    {
        gameObject.transform.position = _pointRocketOnShip.transform.position;
        gameObject.SetActive(true);
    }
}