using System;
using Enemy;
using ShipBase;
using TMPro;
using Unity.VisualScripting;
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
    private RewardedAd _rewardedAd;
    private PopupTutorial _tutorial;

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

        _tutorial.enabled = true;

        UpdateScore();

        _rewardedAd.Rewarded += UpdateScore;
        _exitButton.onClick.AddListener(OnExit);
    }

    private void OnDisable()
    {
        _rewardedAd.Rewarded -= UpdateScore;
        _exitButton.onClick.RemoveListener(OnExit);

        _gameEndHandler.Won -= OnWon;
    }

    public void Init(GameEndHandler gameEndHandler, Wallet wallet, Score score, InterstitialAd interstitialAd,
        RewardedAd rewardedAd,
        PopupTutorial tutorial)
    {
        _gameEndHandler = gameEndHandler;
        _wallet = wallet;
        _score = score;
        _interstitialAd = interstitialAd;
        _rewardedAd = rewardedAd;
        _tutorial = tutorial;

        _gameEndHandler.Won += OnWon;
    }

    private void OnWon()
    {
        _blackout.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void OnExit()
    {
        _interstitialAd.Show();
    }

    private void UpdateScore()
    {
        Debug.Log("Score Updated!");
        _walletView.text = _wallet.Money.ToString();
        _scoreView.text = _score.Points.ToString();
    }
}