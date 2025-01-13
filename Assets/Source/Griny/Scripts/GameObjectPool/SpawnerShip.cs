using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
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

        private int number = 0;
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

                if(_enemy.GetComponent<Health>() == true)
                {
                    _enemy.GetComponent<Health>().Died += SpawnSpip;
                    //Debug.Log("1");
                }                
                
                if(_enemy.GetComponent<Movement>() && _enemy.GetComponent<FighterMovement>() == null && _enemy.GetComponent<ScoutMovement>() == null)
                {
                    _enemy.GetComponent<Movement>().OutSight += SpawnSpip;
                    //Debug.Log("2");
                }                

                if(_enemy.TryGetComponent<ScoutMovement>(out var scoutMovement))
                {
                    _enemy.GetComponent<ScoutMovement>().DisabledEnemy += SpawnSpip;
                    //Debug.Log("3");
                }
            }
        }

        private void Start()
        {
            SpawnSpip();
        }

        private void OnDisable()
        {
            if(_enemy.GetComponent<Health>() == true)
            {
                _enemy.GetComponent<Health>().Died -= SpawnSpip;
                
            }
            
            if(_enemy.GetComponent<Movement>() == true)
            {
                _enemy.GetComponent<Movement>().OutSight -= SpawnSpip;
                
            }
            
            if (_enemy.TryGetComponent<ScoutMovement>(out var scoutMovement))
            {
                _enemy.GetComponent<ScoutMovement>().DisabledEnemy -= SpawnSpip;
                
            }
        }

        private void SpawnSpip()
        {
            if (TryGetShip(out GameObject gameObject))
            {
                //Debug.Log(gameObject + "1");

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

            //number++;
            //Debug.Log(number);
        }

        private bool TryGetShip(out GameObject result)
        {
            Debug.Log("индекс " + numberNextShip);
            if (numberNextShip < _pool.Count)
            {
                result = _pool[numberNextShip];

                //if (result.gameObject.activeSelf == true)
                //{
                //    result = null;
                //}

                numberNextShip++;
            }
            else
            {
                numberNextShip = 0;
                result = _pool[numberNextShip];
            }

            //Debug.Log("result " + result);
            
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
