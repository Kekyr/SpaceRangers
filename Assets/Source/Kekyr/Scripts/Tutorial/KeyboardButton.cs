using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class KeyboardButton : MonoBehaviour
{
    [SerializeField] private Sprite[] _states;

    private Image _image;
    private int _currentStateIndex;
    
    private void Awake()
    {
        if (_states.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_states));
        }

        _image = GetComponent<Image>();
        _image.sprite = _states[_currentStateIndex];
    }

    public void ChangeState()
    {
        _currentStateIndex++;

        if (_currentStateIndex >= _states.Length)
        {
            _currentStateIndex = 0;
        }

        _image.sprite = _states[_currentStateIndex];
    }
}
