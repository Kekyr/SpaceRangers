using ShipBase;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardButton : MonoBehaviour
{
    private Wallet _wallet;
    private Button _button;
    private RewardedAd _rewardedAd;

    private void Start()
    {
        _button = GetComponent<Button>();

        if (_wallet.Money == 0)
        {
            _button.interactable = false;
        }
        
        _button.onClick.AddListener(OnClick);
        _rewardedAd.Closed += OnClosed;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
        _rewardedAd.Closed -= OnClosed;
    }

    public void Init(RewardedAd rewardedAd, Wallet wallet)
    {
        _rewardedAd = rewardedAd;
        _wallet = wallet;
        enabled = true;
    }

    private void OnClick()
    {
        _rewardedAd.Show();
    }

    private void OnClosed()
    {
        _button.interactable = false;
    }
}