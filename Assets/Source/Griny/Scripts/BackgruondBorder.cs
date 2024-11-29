using UnityEngine;

namespace WordGame
{
    public class BackgruondBorder : MonoBehaviour
    {
        [SerializeField] private string _nameBorder;

        public string GetName()
        {
            return _nameBorder;
        }
    }
}