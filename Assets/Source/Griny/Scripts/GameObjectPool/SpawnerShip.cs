using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Enemy
{
    public class SpawnerShip : MonoBehaviour
    {
        //private const float _delyeCoroutine = 5f;

        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private Transform _pointSpawner;


        private List<GameObject> _pool = new List<GameObject>();

        private int numberNextShip = 0;
        private GameObject _enemy;
        //private WaitForSeconds _timeCoroutine = new WaitForSeconds(_delyeCoroutine);
        //private Coroutine _coroutine;

        private void Awake()
        {
            Initialize(_prefabs, _pointSpawner);
        }

        private void Initialize(List<GameObject> objectes, Transform spawnerPisition)
        {
            foreach (GameObject objecte in objectes)
            {
                _enemy = Instantiate(objecte, spawnerPisition);

                _enemy.gameObject.SetActive(false);

                _pool.Add(_enemy);

                _enemy.GetComponent<Health>().Died += SpawnSpip;
                _enemy.GetComponent<Movement>().OutSight += SpawnSpip;

                if(_enemy.TryGetComponent<ScoutMovement>(out var scoutMovement))
                {
                    _enemy.GetComponent<ScoutMovement>().DisabledEnemy += SpawnSpip;
                }
            }
        }

        private void Start()
        {
            SpawnSpip();
        }

        private void OnDisable()
        {
            _enemy.GetComponent<Health>().Died -= SpawnSpip;
            _enemy.GetComponent<Movement>().OutSight -= SpawnSpip;

            if (_enemy.TryGetComponent<ScoutMovement>(out var scoutMovement))
            {
                _enemy.GetComponent<ScoutMovement>().DisabledEnemy -= SpawnSpip;
            }
        }

        private void SpawnSpip()
        {
            if (TryGetShip(out GameObject gameObject))
            {
                SetGameObject(gameObject, _pointSpawner.position);
            }
        }

        private void SetGameObject(GameObject enemy, Vector3 spawnPosition)
        {
            enemy.GetComponent<Health>().ReStartHealth();

            if(enemy.GetComponent<Sheeld>() == true)
            {
                enemy.GetComponent<Sheeld>().ReStartValue();
            }
            
            enemy.gameObject.SetActive(true);
            enemy.transform.position = spawnPosition;
        }

        private bool TryGetShip(out GameObject result)
        {
            if (_pool.Count > numberNextShip)
            {
                result = _pool[numberNextShip];

                if (result.gameObject.activeSelf == true)
                {
                    result = null;
                }

                numberNextShip++;
            }
            else
            {
                numberNextShip = 0;
                result = _pool[numberNextShip];
            }

            //Debug.Log(result + " resulte");
            return result != null;
        }

        private void Update()
        {
            //Debug.Log(numberNextShip);
            //Debug.Log(_pool[1]);
        }

        //private IEnumerator ShootBullet()
        //{
        //    while (true)
        //    {
        //        if (TryGetShip(out GameObject gameObject))
        //        {
        //            SetGameObject(gameObject, _pointSpawner.position);
        //        }

        //        yield return _timeCoroutine;
        //    }
        //}
    }
}
