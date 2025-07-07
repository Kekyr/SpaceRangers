using System;
using UnityEngine;

namespace Tutorial
{
    public class TutorialHand : MonoBehaviour
    {
        public event Action Clicked;

        private void OnMouseDown()
        {
            Clicked?.Invoke();
        }
    }
}