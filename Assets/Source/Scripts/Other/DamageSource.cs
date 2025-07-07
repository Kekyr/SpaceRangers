using System;
using UnityEngine;

namespace Game
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField] private Color _damageColor;
        [SerializeField] private float _damage;

        public float Damage => _damage;
        public Color DamageColor => _damageColor;

        private void OnEnable()
        {
            if (_damage == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(_damage));
            }
        }
    }
}