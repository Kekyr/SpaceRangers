using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Ad
{
    public class InterstitialAd : MonoBehaviour
    {
        private void OnEnable()
        {
            YandexGame.OpenFullAdEvent += OnOpenCallback;
            YandexGame.CloseFullAdEvent += OnCloseCallback;
        }

        private void OnDisable()
        {
            YandexGame.OpenFullAdEvent -= OnOpenCallback;
            YandexGame.CloseFullAdEvent -= OnCloseCallback;
        }

        public void Show()
        {
            int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

            YandexGame.FullscreenShow();
            SceneManager.LoadScene(previousSceneIndex);
        }

        private void OnOpenCallback()
        {
            Time.timeScale = 0;
        }

        private void OnCloseCallback()
        {
            Time.timeScale = 1;
        }
    }
}