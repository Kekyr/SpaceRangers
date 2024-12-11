using System;
using UnityEngine;

namespace ShipBase
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ImprovementsSO _bulletData;
        [SerializeField] private ImprovementsSO _shipData;
        [SerializeField] private AutoGunsZone _autoGunsZone;

        private void Validate()
        {
            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_bulletData == null)
            {
                throw new ArgumentNullException(nameof(_bulletData));
            }

            if (_shipData == null)
            {
                throw new ArgumentNullException(nameof(_shipData));
            }

            if (_autoGunsZone == null)
            {
                throw new ArgumentNullException(nameof(_autoGunsZone));
            }
        }

        private void Awake()
        {
            Validate();

            GameObject ship = Instantiate(_shipData.CurrentPrefab);
            Movement shipMovement = ship.GetComponent<Movement>();
            ObjectPool shipPool = ship.GetComponentInChildren<ObjectPool>();

            if (ship.TryGetComponent(out AutoGuns autoGuns))
            {
                autoGuns.Init(_autoGunsZone);
            }

            shipMovement.Init(_camera);
            shipPool.Init(_bulletData.CurrentPrefab);
        }
    }
}