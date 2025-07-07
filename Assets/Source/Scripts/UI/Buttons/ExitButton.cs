using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class ExitButton : MonoBehaviour
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;

            Time.timeScale = 1;
            SceneManager.LoadScene(previousSceneIndex);
        }
    }
}