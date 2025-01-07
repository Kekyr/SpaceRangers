using UnityEngine;

namespace ShipBase
{
    [CreateAssetMenu(fileName = "new ShieldDataSO", menuName = "ShieldDataSO/Create new ShieldDataSO")]
    public class ShieldDataSO : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _delay;
        [SerializeField] private float _speed;

        public int MaxHealth => _maxHealth;
        public int Delay => _delay;
        public float Speed => _speed;
    }
}