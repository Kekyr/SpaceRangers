using System;
using UnityEngine;

namespace ShipBase
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Movement _shipMovement;
        [SerializeField] private ObjectPool _shipPool;
        [SerializeField] private BulletSO _bulletData;

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

            if (_shipPool == null)
            {
                throw new ArgumentNullException(nameof(_shipPool));
            }

            if (_bulletData == null)
            {
                throw new ArgumentNullException(nameof(_bulletData));
            }
        }

        private void Awake()
        {
            Validate();
            
            _shipMovement.Init(_camera);
            _shipPool.Init(_bulletData.CurrentPrefab);
        }
    }
}