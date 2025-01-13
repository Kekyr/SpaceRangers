using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class Gun : MonoBehaviour
    {
        private const float _delyeCoroutine = 0.5f;

        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _pointSpawner;
        [SerializeField] private Transform _direction;
        [SerializeField] private int _capasity;

        private List<GameObject> _pool = new List<GameObject>();

        private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
        private Coroutine _coroutine;

        private void Awake()
        {
            Initialize(_prefab, _pointSpawner);
        }

        private void Start()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }

            _coroutine = StartCoroutine(ShootBullet());
        }

        private void Initialize(GameObject prefab, Transform spavnerPosition)
        {
            for (int i = 0; i < _capasity; i++)
            {
                GameObject spawned = Instantiate(prefab, spavnerPosition);

                spawned.gameObject.SetActive(false);

                _pool.Add(spawned);
            }
        }

        private IEnumerator ShootBullet()
        {
            while (true)
            {
                if (TryGetBoard(out GameObject gameObject))
                {
                    SetGameObject(gameObject, _pointSpawner.position);
                }

                yield return _timeCoroutine;
            }
        }

        private void SetGameObject(GameObject bullet, Vector3 spawnPosition)
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = spawnPosition;
            bullet.transform.rotation = gameObject.transform.rotation;

            if(bullet.GetComponent<Bullet>() != null)
            {
                bullet.GetComponent<Bullet>().SetVector(-_direction.transform.localPosition);
            }
        }

        private bool TryGetBoard(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return result != null;
        }
    }
}