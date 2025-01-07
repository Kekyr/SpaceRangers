using System;
using UnityEngine;

namespace ShipBase
{
    public class Wallet : MonoBehaviour
    {
        private int _money;

        public Action<int> Changed;

        public void Add(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            _money += amount;
            Changed?.Invoke(_money);
        }
    }
}
