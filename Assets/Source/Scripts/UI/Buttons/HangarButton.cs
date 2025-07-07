using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HangarButton : MonoBehaviour
    {
        [SerializeField] private HangarPopup _hangarPopup;

        private Button _button;

        private void Awake()
        {
            if (_hangarPopup == null)
            {
                throw new ArgumentNullException(nameof(_hangarPopup));
            }

            _button = GetComponent<Button>();

            _button.onClick.AddListener(_hangarPopup.OnOpen);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(_hangarPopup.OnOpen);
        }
    }
}