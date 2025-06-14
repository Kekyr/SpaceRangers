using System;
using ShipBase;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardRocketLauncherTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _spaceButton;
    [SerializeField] private GameObject[] _explanations;
    [SerializeField] private Button _pauseButton;

    private TutorialStepSO _tutorialData;
    private RocketLauncher _rocketLauncher;

    private void Start()
    {
        if (_spaceButton == null)
        {
            throw new ArgumentNullException(nameof(_spaceButton));
        }

        if (_explanations.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_explanations));
        }

        if (_pauseButton == null)
        {
            throw new ArgumentNullException(nameof(_pauseButton));
        }

        if (_tutorialData.IsCompleted == true)
        {
            return;
        }
        
        Time.timeScale = 0f;
        _pauseButton.interactable = false;
        
        _spaceButton.SetActive(true);

        for (int i = 0; i < _explanations.Length; i++)
        {
            _explanations[i].gameObject.SetActive(true);
        }

        _rocketLauncher.Launched += OnLaunched;
    }

    private void OnDisable()
    {
        _rocketLauncher.Launched -= OnLaunched;
    }

    public void Init(TutorialStepSO tutorialData, RocketLauncher rocketLauncher)
    {
        _tutorialData = tutorialData;
        _rocketLauncher = rocketLauncher;
    }

    private void OnLaunched()
    {
        Debug.Log("Tutorial Completed!");
        _pauseButton.interactable = true;
        _tutorialData.Completed();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
