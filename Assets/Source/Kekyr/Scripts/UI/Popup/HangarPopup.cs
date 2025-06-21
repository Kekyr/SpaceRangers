using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _walletView;
    [SerializeField] private TextMeshProUGUI _coinsCount;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _blackout;

    [SerializeField] private ImprovementsView[] _improvementsViews;
    
    private IImprovementsSO[] _improvementsData;
    private WalletSO _walletData;
    private SaveLoader _saveLoader;

    public event Action Bought;

    private void Start()
    {
        if (_walletView == null)
        {
            throw new ArgumentNullException(nameof(_walletView));
        }
        
        if (_coinsCount == null)
        {
            throw new ArgumentNullException(nameof(_coinsCount));
        }

        if (_closeButton == null)
        {
            throw new ArgumentNullException(nameof(_closeButton));
        }

        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }

        if (_improvementsViews.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_improvementsViews));
        }

        for (int i = 0; i < _improvementsData.Length; i++)
        {
            _improvementsViews[i].Init(_improvementsData[i],this);
            _improvementsViews[i].Clicked += OnClicked;
        }

        _coinsCount.text = _walletData.Money.ToString();
        _closeButton.onClick.AddListener(OnClose);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _improvementsData.Length; i++)
        {
            _improvementsViews[i].Clicked -= OnClicked;
        }
        
        _closeButton.onClick.RemoveListener(OnClose);
    }

    public void Init(WalletSO walletData, SaveLoader saveLoader,IImprovementsSO[] improvementsData)
    {
        _walletData = walletData;
        _saveLoader = saveLoader;
        _improvementsData = improvementsData;
        enabled = true;
    }

    public void OnOpen()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OnClose()
    {
        _blackout.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    
    private void OnClicked(IImprovementsSO improvementsData, ImprovementDataSO improvementData)
    {
        if (TryBuy(improvementData.Price) == true)
        {
            Buy(improvementData.Price);
            improvementsData.SetCurrent(improvementData);
            _saveLoader.Save();
            Bought?.Invoke();
        }
    }

    private void Buy(int price)
    {
        _walletData.Decrease(price);
        _coinsCount.text = _walletData.Money.ToString();
        _walletView.text = _walletData.Money.ToString();
    }

    private bool TryBuy(int price)
    {
        if (price < 0)
        {
            return false;
        }

        return _walletData.Money - price >= 0;
    }
}