using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementView : MonoBehaviour
{
    [SerializeField] private Image _skill;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _price;

    private ImprovementDataSO _data;

    public event Action<ImprovementDataSO> Clicked;

    private void Start()
    {
        if (_skill == null)
        {
            throw new ArgumentNullException(nameof(_skill));
        }

        if (_button == null)
        {
            throw new ArgumentNullException(nameof(_button));
        }

        if (_price == null)
        {
            throw new ArgumentNullException(nameof(_price));
        }

        _price.text = _data.Price.ToString();
        _button.onClick.AddListener(OnClick);

        CheckState();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void Init(ImprovementDataSO data)
    {
        _data = data;
        enabled = true;
    }

    private void CheckState()
    {
        if (_data.isOpened == false)
        {
            return;
        }

        ChangeColor(_skill, Color.white);
        
        _button.interactable = true;

        if (_data.isBought == false)
        {
            return;
        }
    }

    private void ChangeColor(Image image, Color color)
    {
        image.color = color;
    }

    private void OnClick()
    {
        Clicked?.Invoke(_data);
    }
}
