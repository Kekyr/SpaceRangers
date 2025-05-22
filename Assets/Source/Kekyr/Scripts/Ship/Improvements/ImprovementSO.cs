using UnityEngine;

namespace ShipBase
{
    public abstract class ImprovementSO<T> : ImprovementDataSO
    {
        [SerializeField] private T _value;

        public T Value => _value;
    }
}
