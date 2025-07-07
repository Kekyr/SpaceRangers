using Game;
using TMPro;
using UnityEngine;

namespace WalletSystem
{
    public class WalletView : MonoBehaviour
    {
        private TextMeshProUGUI _textMeshPro;
        private Wallet _wallet;
        private GameEndHandler _gameEndHandler;

        private void OnEnable()
        {
            _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            _wallet.Changed += OnChanged;
            _gameEndHandler.Won += OnWon;
        }

        private void OnDisable()
        {
            _wallet.Changed -= OnChanged;
            _gameEndHandler.Won -= OnWon;
        }

        public void Init(Wallet wallet, GameEndHandler gameEndHandler)
        {
            _wallet = wallet;
            _gameEndHandler = gameEndHandler;
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