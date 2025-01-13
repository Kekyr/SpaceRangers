using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private int _capasity;

        private List<GameObject> _pool = new List<GameObject>();

        private int numberNextShip = 0;

        protected void Initialize(GameObject prefab, Transform spavnerPosition)
        {
            for (int i = 0; i < _capasity; i++)
            {
                GameObject spawned = Instantiate(prefab, spavnerPosition);

                spawned.gameObject.SetActive(false);

                _pool.Add(spawned);
            }
        }

        protected void Initialize(List<GameObject> objectes, Transform spawnerPisition)
        {
            foreach(GameObject objecte in objectes)
            {
                GameObject spawned = Instantiate(objecte, spawnerPisition);

                spawned.gameObject.SetActive(false);

                //spawned.GetComponent<Health>().Died += SetEnemy;

                _pool.Add(spawned);
            }
        }

        protected bool TryGetBoard(out GameObject result)
        {
            result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

            return result != null;
        }

        protected bool TryGetShip(out GameObject result)
        {
            if(_pool.Count > numberNextShip)
            {
                result = _pool[numberNextShip];

                if(result.gameObject.activeSelf == true)
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

            return result != null;
        }
    }
}