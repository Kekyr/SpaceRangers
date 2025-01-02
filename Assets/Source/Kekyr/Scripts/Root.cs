using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShipBase
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private AutoGunsZone _autoGunsZone;
        [SerializeField] private Button _addRocketButton;

        [SerializeField] private HealthView _healthView;

        [SerializeField] private ImprovementsSO<GameObject> _bulletData;
        [SerializeField] private ImprovementsSO<GameObject> _shipData;
        [SerializeField] private ImprovementsSO<int> _rocketData;

        private void Validate()
        {
            if (_camera == null)
            {
                throw new ArgumentNullException(nameof(_camera));
            }

            if (_autoGunsZone == null)
            {
                throw new ArgumentNullException(nameof(_autoGunsZone));
            }

            if (_addRocketButton == null)
            {
                throw new ArgumentNullException(nameof(_addRocketButton));
            }

            if (_bulletData == null)
            {
                throw new ArgumentNullException(nameof(_bulletData));
            }

            if (_shipData == null)
            {
                throw new ArgumentNullException(nameof(_shipData));
            }

            if (_rocketData == null)
            {
                throw new ArgumentNullException(nameof(_rocketData));
            }

            if (_healthView == null)
            {
                throw new ArgumentNullException(nameof(_healthView));
            }
        }

        private void Awake()
        {
            Validate();

            GameObject ship = Instantiate(_shipData.CurrentLevel);
            Movement shipMovement = ship.GetComponent<Movement>();
            Health shipHealth = ship.GetComponent<Health>();
            RocketLauncher shipRocketLauncher = ship.GetComponentInChildren<RocketLauncher>();
            BulletPool shipPool = ship.GetComponentInChildren<BulletPool>();

            if (ship.TryGetComponent(out AutoGuns autoGuns))
            {
                autoGuns.Init(_autoGunsZone);
            }

            shipMovement.Init(_camera);
            shipRocketLauncher.Init(_camera, _addRocketButton, _rocketData.CurrentLevel);
            shipPool.Init(_bulletData.CurrentLevel);

            _healthView.Init(shipHealth);
        }
    }
}