using TMPro;
using UnityEngine;

namespace ShipBase
{
    public class WalletView : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;
        private Wallet _wallet;

        private void OnEnable()
        {
            _textMeshPro = GetComponent<TextMeshProUGUI>();
            _wallet.Changed += OnChanged;
        }

        private void OnDisable()
        {
            _wallet.Changed -= OnChanged;
        }

        public void Init(Wallet wallet)
        {
            _wallet = wallet;
            enabled = true;
        }

        private void OnChanged(int newValue)
        {
            _textMeshPro.text = newValue.ToString();
        }
    }
}