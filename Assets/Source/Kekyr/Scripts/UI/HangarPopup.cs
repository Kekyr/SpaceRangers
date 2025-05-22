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
        
        _bulletsView.Init(_bulletImprovementsData.Levels);
        _shieldsView.Init(_shieldImprovementsData.Levels);
        _shipsView.Init(_shipImprovementsData.Levels);
        _rocketsView.Init(_rocketImprovementsData.Levels);

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

    public void Init(WalletSO walletData,ImprovementsSO<GameObject> bulletImprovementsData,
        ImprovementsSO<ShieldDataSO> shieldImprovementsData,ImprovementsSO<GameObject> shipImprovementsData,
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
        _bulletImprovementsData.SetCurrent(data);
    }
    
    private void OnShieldClicked(ImprovementDataSO data)
    {
        _shieldImprovementsData.SetCurrent(data);
    }
    
    private void OnShipClicked(ImprovementDataSO data)
    {
        _shipImprovementsData.SetCurrent(data);
    }
    
    private void OnRocketClicked(ImprovementDataSO data)
    {
        _rocketImprovementsData.SetCurrent(data);
    }
}