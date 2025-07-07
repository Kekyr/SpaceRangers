using System;
using ShipBase;
using UnityEngine;

namespace UI
{
    public class ImprovementsView : MonoBehaviour
    {
        [SerializeField] private ImprovementView[] _views;

        private IImprovementsSO _data;
        private ImprovementDataSO[] _levels;
        private HangarPopup _hangarPopup;

        public event Action<IImprovementsSO, ImprovementDataSO> Clicked;

        private void Start()
        {
            if (_views.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_views));
            }

            _levels = _data.Levels;

            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].Init(_levels[i + 1]);
                _views[i].Clicked += OnClicked;
            }

            SetStates();

            _hangarPopup.Bought += SetStates;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _views.Length; i++)
            {
                _views[i].Clicked -= OnClicked;
            }

            _hangarPopup.Bought -= SetStates;
        }

        public void Init(IImprovementsSO data, HangarPopup hangarPopup)
        {
            _data = data;
            _hangarPopup = hangarPopup;
            enabled = true;
        }

        private void OnClicked(ImprovementDataSO improvementData)
        {
            Clicked?.Invoke(_data, improvementData);
        }

        private void SetStates()
        {
            if (_data.CurrentIndex != 0)
            {
                _views[_data.CurrentIndex - 1].Choose();
            }

            if (_data.CurrentIndex > 1)
            {
                for (int i = 0; i < _data.CurrentIndex - 1; i++)
                {
                    _views[i].Buy();
                }
            }

            if (_data.CurrentIndex < _views.Length)
            {
                _views[_data.CurrentIndex].Open();
            }
        }
    }
}