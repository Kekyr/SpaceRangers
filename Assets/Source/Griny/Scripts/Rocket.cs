using Enemy;
using System.Collections;
using UnityEngine;
using WordGame;

[RequireComponent(typeof(Rigidbody2D))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _direction;
    [SerializeField] private Transform _pointRocketOnShip;

    private Coroutine _coroutine;

    public void RunRocket()
    {
        Debug.Log("в методе корутыны");

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(FlyRocket());
    }

    private IEnumerator FlyRocket()
    {
        Debug.Log("в корутине");
        gameObject.transform.parent = null;

        while (_speed != 0)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.down * _speed * Time.deltaTime);

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out BackgroundBorder border))
        {
            if(border.GetName() == "down")
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
        Debug.Log(gameObject.transform.position + "рестарт");
        gameObject.SetActive(true);
    }
}
