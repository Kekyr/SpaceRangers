using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationRoot : MonoBehaviour
{
    private void Start()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}