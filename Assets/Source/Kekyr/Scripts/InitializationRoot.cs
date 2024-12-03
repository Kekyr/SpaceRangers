using UnityEngine;
using UnityEngine.SceneManagement;

public class InitializationRoot : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}