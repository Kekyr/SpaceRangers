using System;
using UnityEngine;

public class ImprovementsView : MonoBehaviour
{
    [SerializeField] private ImprovementView[] _views;
    
    private ImprovementDataSO[] _data;
    private HangarPopup _hangarPopup;

    public event Action<ImprovementDataSO> Clicked;

    private void Start()
    {
        if (_views.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_views));
        }

        _hangarPopup.Bought += OnBought;

        for (int i = 0; i < _views.Length; i++)
        {
            _views[i].Init(_data[i+1]);
            _views[i].Clicked += OnClicked;
        }
    }

    private void OnDestroy()
    {
        _hangarPopup.Bought -= OnBought;
    }

    public void Init(ImprovementDataSO[] data, HangarPopup hangarPopup)
    {
        _data = data;
        _hangarPopup = hangarPopup;
        enabled = true;
    }

    private void OnClicked(ImprovementDataSO data)
    {
        Clicked?.Invoke(data);
    }

    private void OnBought()
    {
        for (int i = 0; i < _views.Length; i++)
        {
            _views[i].CheckState();
        }
    }
}