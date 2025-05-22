using System;
using UnityEngine;

public class ImprovementsView : MonoBehaviour
{
    [SerializeField] private ImprovementView[] _views;
    
    private ImprovementDataSO[] _data;

    public event Action<ImprovementDataSO> Clicked;

    private void Start()
    {
        if (_views.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_views));
        }

        for (int i = 0; i < _views.Length; i++)
        {
            _views[i].Init(_data[i+1]);
            _views[i].Clicked += OnClicked;
        }
    }

    public void Init(ImprovementDataSO[] data)
    {
        _data = data;
        enabled = true;
    }

    private void OnClicked(ImprovementDataSO data)
    {
        Clicked?.Invoke(data);
    }
}