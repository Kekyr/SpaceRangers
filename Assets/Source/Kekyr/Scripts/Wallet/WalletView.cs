using TMPro;
using UnityEngine;

namespace ShipBase
{
    public class WalletView : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;
        private Wallet _wallet;
        private WinHandler _winHandler;

        private void OnEnable()
        {
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            _wallet.Changed += OnChanged;
            _winHandler.Won += OnWon;
        }

        private void OnDisable()
        {
            _wallet.Changed -= OnChanged;
            _winHandler.Won -= OnWon;
        }

        public void Init(Wallet wallet, WinHandler winHandler)
        {
            _wallet = wallet;
            _winHandler = winHandler;
            enabled = true;
        }

        private void OnChanged(int newValue)
        {
            _textMeshPro.text = newValue.ToString();
        }

        private void OnWon()
        {
            gameObject.SetActive(false);
        }
    }
}