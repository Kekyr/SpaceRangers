using System;
using Enemy;
using ShipBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _walletView;
    [SerializeField] private TextMeshProUGUI _scoreView;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Image _blackout;

    private GameEndHandler _gameEndHandler;
    private Wallet _wallet;
    private Score _score;
    private EnemyShip _boss;
    private InterstitialAd _interstitialAd;
    private SaveLoader _saveLoader;

    public event Action Exited;

    private void Awake()
    {
        if (_wallet == null)
        {
            throw new ArgumentNullException(nameof(_wallet));
        }

        if (_score == null)
        {
            throw new ArgumentNullException(nameof(_score));
        }

        if (_exitButton == null)
        {
            throw new ArgumentNullException(nameof(_exitButton));
        }

        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }
        
        _exitButton.onClick.AddListener(OnExit);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExit);
        
        _gameEndHandler.Won -= OnWon;
        _wallet.Changed -= OnWalletChanged;
        _score.Changed -= OnScoreChanged;
    }

    public void Init(GameEndHandler gameEndHandler, Wallet wallet, Score score, InterstitialAd interstitialAd,SaveLoader saveLoader)
    {
        _gameEndHandler = gameEndHandler;
        _wallet = wallet;
        _score = score;
        _interstitialAd = interstitialAd;
        _saveLoader = saveLoader;

        _gameEndHandler.Won += OnWon;
        _wallet.Changed += OnWalletChanged;
        _score.Changed += OnScoreChanged;
    }

    private void OnWon()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OnWalletChanged(int newValue)
    {
        _walletView.text = newValue.ToString();
    }
    
    private void OnScoreChanged(int newValue)
    {
        _scoreView.text = newValue.ToString();
    }

    private void OnExit()
    {
        Exited?.Invoke();
        _saveLoader.Save();
        _interstitialAd.Show();
    }
}