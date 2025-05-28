using UnityEngine;

[CreateAssetMenu(fileName = "new WalletSO", menuName = "WalletSO/Create new WalletSO")]
public class WalletSO : ScriptableObject
{
    [SerializeField] private int _money;

    public int Money => _money;

    public void Init(int money)
    {
        _money = money;
    }
    
    public void Add(int count)
    {
        _money += count;
    }

    public void Decrease(int count)
    {
        _money -= count;
    }

    public void Reset()
    {
        _money = 0;
    }
}
