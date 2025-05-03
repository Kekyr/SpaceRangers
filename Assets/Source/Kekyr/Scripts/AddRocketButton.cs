using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddRocketButton : MonoBehaviour
{
    private RewardedAd _rewardedAd;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        
        _button.onClick.AddListener(_rewardedAd.Show);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(_rewardedAd.Show);
    }

    public void Init(RewardedAd rewardedAd)
    {
        _rewardedAd = rewardedAd;
        enabled = true;
    }
}