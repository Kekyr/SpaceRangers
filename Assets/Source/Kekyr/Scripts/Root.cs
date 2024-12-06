using System;
using UnityEngine;

namespace ShipBase
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Movement _shipMovement;

        private void Validate()
        {
            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_shipMovement == null)
            {
                throw new ArgumentNullException(nameof(_shipMovement));
            }
        }

        private void Awake()
        {
            Validate();
            
            _shipMovement.Init(_camera);
        }
    }
}