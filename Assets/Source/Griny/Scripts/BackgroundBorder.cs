using UnityEngine;

namespace WordGame
{
    public class BackgroundBorder : MonoBehaviour
    {
        [SerializeField] private string _nameBorder;

        public string GetName()
        {
            return _nameBorder;
        }
    }
}