using System;
using Audio;
using Enemy;
using ShipBase;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(SFX))]
public class WinPopup : MonoBehaviour
{
    [SerializeField] private SFXSO _winSfx;
    [SerializeField] private TextMeshProUGUI _walletView;
    [SerializeField] private TextMeshProUGUI _scoreView;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Image _blackout;

    private WinHandler _winHandler;
    private Wallet _wallet;
    private Score _score;
    private EnemyShip _boss;
    private SFX _sfx;
    private AudioSettingSO _sfxSetting;

    public event Action Exited;

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

        if (_exitButton == null)
        {
            throw new ArgumentNullException(nameof(_exitButton));
        }

        if (_blackout == null)
        {
            throw new ArgumentNullException(nameof(_blackout));
        }
        
        _exitButton.onClick.AddListener(OnExit);

        _sfx = GetComponent<SFX>();
        _sfx.Init(_sfxSetting);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExit);
        
        _winHandler.Won -= OnWon;
        _wallet.Changed -= OnWalletChanged;
        _score.Changed -= OnScoreChanged;
    }

    public void Init(WinHandler winHandler, Wallet wallet, Score score, AudioSettingSO sfxSetting)
    {
        _winHandler = winHandler;
        _wallet = wallet;
        _score = score;
        _sfxSetting = sfxSetting;

        _winHandler.Won += OnWon;
        _wallet.Changed += OnWalletChanged;
        _score.Changed += OnScoreChanged;
    }

    private void OnWon()
    {
        _blackout.gameObject.SetActive(true);
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

    private void OnExit()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

        Time.timeScale = 1f;
        Exited?.Invoke();
        SceneManager.LoadScene(previousSceneIndex);
    }
}