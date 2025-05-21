using System;
using LevelEnemy;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsView : MonoBehaviour
{
    [SerializeField] private LevelView[] _views;
    
    private LevelsSO _levelsData;

    private void Start()
    {
        if (_views.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_views));
        }

        for (int i = 0; i < _levelsData.Data.Count; i++)
        {
            _views[i].Init(_levelsData.Data[i]);
            _views[i].Clicked += OnClicked;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _levelsData.Data.Count; i++)
        {
            _views[i].Clicked -= OnClicked;
        }
    }

    public void Init(LevelsSO data)
    {
        _levelsData = data;
        enabled = true;
    }

    private void OnClicked(LevelSO data)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        _levelsData.SetCurrent(data);
        SceneManager.LoadScene(nextSceneIndex);
    }
}