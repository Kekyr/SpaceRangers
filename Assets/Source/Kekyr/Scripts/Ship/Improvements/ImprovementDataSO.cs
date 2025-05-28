using UnityEngine;

public abstract class ImprovementDataSO : ScriptableObject
{
    [SerializeField] private int _price;
    [SerializeField] private bool _isBought;
    [SerializeField] private bool _isOpened;
    [SerializeField] private bool _isCurrent;

    public int Price => _price;
    public bool isBought => _isBought;
    public bool isOpened => _isOpened;
    public bool isCurrent => _isCurrent;
    
    public void Buy()
    {
        _isBought = true;
    }

    public void Open()
    {
        _isOpened = true;
    }

    public void Choose()
    {
        _isCurrent = true;
    }

    public void UnChoose()
    {
        _isCurrent = false;
    }

    public void Reset()
    {
        _isBought = false;
        _isOpened = false;
        _isCurrent = false;
    }
}
