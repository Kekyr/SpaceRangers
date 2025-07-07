using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImprovementView : MonoBehaviour
{
    private readonly Color _current = new Color(0.490566f, 0.4681433f, 0.3309007f, 0.6588235f);

    [SerializeField] private Image _image;
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _price;
    [SerializeField] private Animator _animator;

    private ImprovementDataSO _data;

    public event Action<ImprovementDataSO> Clicked;

    private void Start()
    {
        if (_image == null)
        {
            throw new ArgumentNullException(nameof(_image));
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

    public void Open()
    {
        _button.interactable = true;
    }

    public void Buy()
    {
        _image.gameObject.SetActive(false);
        _button.interactable = false;
    }

    public void Choose()
    {
        _image.color = _current;
        _image.gameObject.SetActive(true);
        _animator.enabled = true;
        _button.interactable = false;
    }

    private void OnClick()
    {
        Clicked?.Invoke(_data);
    }
}