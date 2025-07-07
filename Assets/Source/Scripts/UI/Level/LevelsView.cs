using System;
using Background;
using Level;
using SaveSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelsView : MonoBehaviour
    {
        [SerializeField] private LevelView[] _views;

        private LevelsSO _levelsData;
        private BackgroundSO _backgroundData;
        private SaveLoader _saveLoader;

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
                _views[i].Checked += OnChecked;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _levelsData.Data.Count; i++)
            {
                _views[i].Clicked -= OnClicked;
                _views[i].Checked -= OnChecked;
            }
        }

        public void Init(LevelsSO data, BackgroundSO backgroundData, SaveLoader saveLoader)
        {
            _levelsData = data;
            _backgroundData = backgroundData;
            _saveLoader = saveLoader;
            enabled = true;
        }

        private void OnChecked()
        {
            _views[_levelsData.CurrentIndex].MakeCurrent();
        }

        private void OnClicked(LevelSO data)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            _levelsData.SetCurrent(data);
            _backgroundData.SetCurrent(_levelsData.CurrentIndex);
            _saveLoader.Save();

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}