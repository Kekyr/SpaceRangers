using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class InitializationRoot : MonoBehaviour
{
    private void Start()
    {
        YandexGame.GameReadyAPI();
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}