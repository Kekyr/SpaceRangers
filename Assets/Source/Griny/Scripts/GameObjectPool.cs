using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class GameObjectPool : MonoBehaviour
    {
        private const float _delyeCoroutine = 0.5f;

        [SerializeField] private int _capasity;

        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _pointSpawner;
        [SerializeField] private Transform _direction;
        [SerializeField] private float _delay;
        //[SerializeField] private GameObject _ship;

        private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
        private List<Bullet> _pool = new List<Bullet>();
        private Coroutine _coroutine;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ShootBullet());
        }

        private void SetDesk(Bullet bullet, Vector3 spawnPosition)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = spawnPosition;
            bullet.transform.rotation = gameObject.transform.rotation;
            bullet.SetVector(-_direction.transform.localPosition);
        }

        private void Initialize()
        {
            for (int i = 0; i < _capasity; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            Bullet spawned = Instantiate(_prefab, _pointSpawner/*.position*//*,*/ /*_ship.transform.rotation, _pointSpawner*/);

            spawned.gameObject.SetActive(false);

            _pool.Add(spawned);
        }

        private bool TryGetBoard(out Bullet result)
        {
            result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return result;
        }

        private IEnumerator ShootBullet()
        {
            while (true)
            {
                if (TryGetBoard(out Bullet gameObject))
                {                  
                    SetDesk(gameObject, _pointSpawner.position);
                    //Debug.Log(_pool[1].Speed);
                }
                yield return _timeCoroutine;
            }
        }
    }
}