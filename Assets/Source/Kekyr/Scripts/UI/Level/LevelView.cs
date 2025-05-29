using System;
using LevelEnemy;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelView : MonoBehaviour
{
    private readonly Color _planetOpened = new Color(0.8773585f, 0.8773585f, 0.8773585f);
    private readonly Color _starsOpened = new Color(0.2745098f, 0.1215686f, 0.1529412f);
    private readonly Color _starsCompleted = new Color(255, 225, 0);
    private readonly Color _planetSelected = new Color(0.3813552f, 0.745283f, 0.2425685f, 0.6588235f);

    [SerializeField] private Image _planet;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Button _button;

    private LevelSO _data;

    public event Action<LevelSO> Clicked;

    private void Start()
    {
        if (_planet == null)
        {
            throw new ArgumentNullException(nameof(_planet));
        }

        if (_stars.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_stars));
        }

        if (_button == null)
        {
            throw new ArgumentNullException(nameof(_button));
        }

        _button.onClick.AddListener(OnClick);

        CheckState();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void Init(LevelSO data)
    {
        _data = data;
        enabled = true;
    }

    private void CheckState()
    {
        if (_data.Status == LevelState.Closed)
        {
            return;
        }

        Color planetColor = _data.IsCurrent == true ? _planetSelected : _planetOpened;
        _planet.color = planetColor;

        Color starColor = _data.Status == LevelState.Completed ? _starsCompleted : _starsOpened;
        
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].color = starColor;
        }
        
        _button.interactable = true;
    }

    private void OnClick()
    {
        Clicked?.Invoke(_data);
    }
}