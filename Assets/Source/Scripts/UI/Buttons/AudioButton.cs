using System;
using SaveSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    [RequireComponent(typeof(Button))]
    public class AudioButton : MonoBehaviour
    {
        private readonly float _OnAlpha = 1f;
        private readonly float _OffAlpha = 0.2f;

        [SerializeField] private Image _icon;

        private Button _button;
        private AudioSettingSO _audioSettingSO;
        private SaveLoader _saveLoader;

        public event Action Switched;

        private void OnEnable()
        {
            if (_icon == null)
            {
                throw new ArgumentNullException(nameof(_icon));
            }

            _button = GetComponent<Button>();
            Sync();
            _button.onClick.AddListener(Switch);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Switch);
        }

        public void Init(AudioSettingSO audioSettingSO, SaveLoader saveLoader)
        {
            _audioSettingSO = audioSettingSO;
            _saveLoader = saveLoader;
            enabled = true;
        }

        private void Switch()
        {
            _audioSettingSO.Switch();
            _saveLoader.Save();
            Sync();
            Switched?.Invoke();
        }

        private void Sync()
        {
            float newValue = _audioSettingSO.IsOn == true ? _OnAlpha : _OffAlpha;
            ChangeAlpha(newValue);
        }

        private void ChangeAlpha(float newValue)
        {
            Color color = _icon.color;
            color.a = newValue;
            _icon.color = color;
        }
    }
}