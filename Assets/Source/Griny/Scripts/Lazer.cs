using UnityEngine;

namespace Enemy
{
    public class Lazer : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public int Damage => _damage;
    }
}