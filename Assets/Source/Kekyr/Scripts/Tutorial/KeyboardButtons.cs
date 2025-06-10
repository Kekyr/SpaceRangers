using UnityEngine;

public class KeyboardButtons : MonoBehaviour
{
    [SerializeField] private KeyboardButton[] _leftButtons;
    [SerializeField] private KeyboardButton[] _upButtons;
    [SerializeField] private KeyboardButton[] _rightButtons;
    [SerializeField] private KeyboardButton[] _downButtons;

    private void OnLeftButton()
    {
        for (int i = 0; i < _leftButtons.Length; i++)
        {
            _leftButtons[i].ChangeState();
        }
    }
    
    private void OnUpButton()
    {
        for (int i = 0; i < _leftButtons.Length; i++)
        {
            _upButtons[i].ChangeState();
        }
    }
    
    private void OnRightButton()
    {
        for (int i = 0; i < _leftButtons.Length; i++)
        {
            _rightButtons[i].ChangeState();
        }
    }

    private void OnDownButton()
    {
        for (int i = 0; i < _leftButtons.Length; i++)
        {
            _downButtons[i].ChangeState();
        }
    }

}
