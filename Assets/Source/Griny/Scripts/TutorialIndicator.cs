using UnityEngine;
using UnityEngine.UI;


namespace WordGame
{
    public class TutorialIndicator : MonoBehaviour
    {
        private const string _indicatorMapValueKey = "indicatorMap";
        private const string _indicatorHangarValueKey = "indicatorHangar";

        [SerializeField] private Image _indicatorOnMap;
        [SerializeField] private string _keyPrefseMap;
        [SerializeField] private Image _indicatorOnHangar;
        [SerializeField] private string _keyPrefseHangar;

        private void Awake()
        {
            CheckSave(_indicatorOnMap, _keyPrefseMap, _indicatorMapValueKey);
        }

        private void OnEnable()
        {
            CheckSave(_indicatorOnHangar, _keyPrefseHangar, _indicatorHangarValueKey);
        }

        private void CheckSave(Image indicator, string key, string valueKey)
        {
            if (PlayerPrefs.HasKey(key) == false)
            {
                PlayIndicator(indicator);

                PlayerPrefs.SetString(key, valueKey);
            }
            else
            {
                DisableIndicator(indicator);
            }
        }

        private void PlayIndicator(Image indicator)
        {
            indicator.gameObject.SetActive(true);
        }

        private void DisableIndicator(Image indicator)
        {
            indicator.gameObject.SetActive(false);
        }
    }
}