using System;
using ShipBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangarPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsCount;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Image _blackout;

    [SerializeField] private ImprovementsView _bulletsView;
    [SerializeField] private ImprovementsView _shieldsView;
    [SerializeField] private ImprovementsView _shipsView;
    [SerializeField] private ImprovementsView _rocketsView;

    private ImprovementsSO<GameObject> _bulletImprovementsData;
    private ImprovementsSO<ShieldDataSO> _shieldImprovementsData;
    private ImprovementsSO<GameObject> _shipImprovementsData;
    private ImprovementsSO<int> _rocketImprovementsData;

    private WalletSO _walletData;

    public event Action Bought;

    private void Start()
    {
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

        if (_bulletsView == null)
        {
            throw new ArgumentOutOfRangeException(nameof(_bulletsView));
        }

        if (_shieldsView == null)
        {
            throw new ArgumentOutOfRangeException(nameof(_shieldsView));
        }

        if (_shipsView == null)
        {
            throw new ArgumentOutOfRangeException(nameof(_shipsView));
        }

        if (_rocketsView == null)
        {
            throw new ArgumentOutOfRangeException(nameof(_shipsView));
        }

        _bulletsView.Init(_bulletImprovementsData.Levels,this);
        _shieldsView.Init(_shieldImprovementsData.Levels,this);
        _shipsView.Init(_shipImprovementsData.Levels,this);
        _rocketsView.Init(_rocketImprovementsData.Levels,this);

        _coinsCount.text = _walletData.Money.ToString();
        _closeButton.onClick.AddListener(OnClose);

        _bulletsView.Clicked += OnBulletClicked;
        _shieldsView.Clicked += OnShieldClicked;
        _shipsView.Clicked += OnShipClicked;
        _rocketsView.Clicked += OnRocketClicked;
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(OnClose);

        _bulletsView.Clicked -= OnBulletClicked;
        _shieldsView.Clicked -= OnShieldClicked;
        _shipsView.Clicked -= OnShipClicked;
        _rocketsView.Clicked -= OnRocketClicked;
    }

    public void Init(WalletSO walletData, ImprovementsSO<GameObject> bulletImprovementsData,
        ImprovementsSO<ShieldDataSO> shieldImprovementsData, ImprovementsSO<GameObject> shipImprovementsData,
        ImprovementsSO<int> rocketImprovementsData)
    {
        _bulletImprovementsData = bulletImprovementsData;
        _shieldImprovementsData = shieldImprovementsData;
        _shipImprovementsData = shipImprovementsData;
        _rocketImprovementsData = rocketImprovementsData;
        _walletData = walletData;
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

    private void OnBulletClicked(ImprovementDataSO data)
    {
        if (TryBuy(data.Price) == true)
        {
            Buy(data.Price);
            _bulletImprovementsData.SetCurrent(data);
            Bought?.Invoke();
        }
    }

    private void OnShieldClicked(ImprovementDataSO data)
    {
        if (TryBuy(data.Price) == true)
        {
            Buy(data.Price);
            _shieldImprovementsData.SetCurrent(data);
            Bought?.Invoke();
        }
    }

    private void OnShipClicked(ImprovementDataSO data)
    {
        if (TryBuy(data.Price) == true)
        {
            Buy(data.Price);
            _shipImprovementsData.SetCurrent(data);
            Bought?.Invoke();
        }
    }

    private void OnRocketClicked(ImprovementDataSO data)
    {
        if (TryBuy(data.Price) == true)
        {
            Buy(data.Price);
            _rocketImprovementsData.SetCurrent(data);
            Bought?.Invoke();
        }
    }

    private void Buy(int price)
    {
        _walletData.Decrease(price);
        _coinsCount.text = _walletData.Money.ToString();
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