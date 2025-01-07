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
        [SerializeField] private ShieldView _shieldView;
        [SerializeField] private WalletView _walletView;

        [SerializeField] private ImprovementsSO<GameObject> _bulletData;
        [SerializeField] private ImprovementsSO<GameObject> _shipData;
        [SerializeField] private ImprovementsSO<int> _rocketData;
        [SerializeField] private ShieldImprovementsSO _shieldData;

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

            if (_shieldData == null)
            {
                throw new ArgumentNullException(nameof(_shieldData));
            }

            if (_healthView == null)
            {
                throw new ArgumentNullException(nameof(_healthView));
            }

            if (_shieldView == null)
            {
                throw new ArgumentNullException(nameof(_shieldView));
            }

            if (_walletView == null)
            {
                throw new ArgumentNullException(nameof(_walletView));
            }
        }

        private void Awake()
        {
            Validate();

            GameObject ship = Instantiate(_shipData.CurrentLevel);

            Movement movement = ship.GetComponent<Movement>();
            Health health = ship.GetComponent<Health>();
            Wallet wallet = ship.GetComponent<Wallet>();
            Shield shield = ship.GetComponentInChildren<Shield>();
            RocketLauncher rocketLauncher = ship.GetComponentInChildren<RocketLauncher>();
            BulletPool pool = ship.GetComponentInChildren<BulletPool>();

            if (ship.TryGetComponent(out AutoGuns autoGuns))
            {
                autoGuns.Init(_autoGunsZone);
            }

            movement.Init(_camera);
            rocketLauncher.Init(_camera, _addRocketButton, _rocketData.CurrentLevel);
            pool.Init(_bulletData.CurrentLevel);
            shield.Init(_shieldData.CurrentLevel);

            _healthView.Init(health);
            _shieldView.Init(shield);
            _walletView.Init(wallet);
        }
    }
}