using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialHand : MonoBehaviour
{
    public event Action Clicked;

    private void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag!");
        
        Clicked?.Invoke();
    }
}