using System;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    public event Action Clicked;
    
    private void OnMouseDown()
    {
        Clicked?.Invoke();
    }
}