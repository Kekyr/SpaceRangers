using UnityEngine;
using YG;

namespace ShipBase
{
    public abstract class ImprovementDataSO : ScriptableObject
    {
        [SerializeField] private int _price;
        [SerializeField] private ImprovementState _state;
        [SerializeField] private bool _isCurrent;

        public int Price => _price;
        public ImprovementState Status => _state;
        public bool isCurrent => _isCurrent;

        public void Init(ImprovementState state)
        {
            _state = state;
        }

        public void Open()
        {
            _state = ImprovementState.Opened;
        }

        public void Buy()
        {
            _state = ImprovementState.Bought;
        }

        public void Choose()
        {
            _isCurrent = true;
        }

        public void UnChoose()
        {
            _isCurrent = false;
        }

        public void Reset()
        {
            _state = ImprovementState.Closed;
            _isCurrent = false;
        }
    }
}
