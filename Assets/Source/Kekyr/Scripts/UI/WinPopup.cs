using System;
using Audio;
using Enemy;
using ShipBase;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SFX))]
public class WinPopup : MonoBehaviour
{
    [SerializeField] private SFXSO _winSfx;
    [SerializeField] private TextMeshProUGUI _walletView;
    [SerializeField] private TextMeshProUGUI _scoreView;

    private WinHandler _winHandler;
    private Wallet _wallet;
    private Score _score;
    private EnemyShip _boss;
    private SFX _sfx;

    private void Awake()
    {
        if (_winSfx == null)
        {
            throw new ArgumentNullException(nameof(_winSfx));
        }

        if (_wallet == null)
        {
            throw new ArgumentNullException(nameof(_wallet));
        }

        if (_score == null)
        {
            throw new ArgumentNullException(nameof(_score));
        }

        _sfx = GetComponent<SFX>();
    }

    private void OnDisable()
    {
        _winHandler.Won -= OnWon;
        _wallet.Changed -= OnWalletChanged;
        _score.Changed -= OnScoreChanged;
    }

    public void Init(WinHandler winHandler, Wallet wallet, Score score)
    {
        _winHandler = winHandler;
        _wallet = wallet;
        _score = score;

        _winHandler.Won += OnWon;
        _wallet.Changed += OnWalletChanged;
        _score.Changed += OnScoreChanged;
    }

    private void OnWon()
    {
        gameObject.SetActive(true);
    }

    private void OnAnimationStarted()
    {
        _sfx.Play(_winSfx);
    }

    private void OnWalletChanged(int newValue)
    {
        _walletView.text = newValue.ToString();
    }
    
    private void OnScoreChanged(int newValue)
    {
        _scoreView.text = newValue.ToString();
    }
}