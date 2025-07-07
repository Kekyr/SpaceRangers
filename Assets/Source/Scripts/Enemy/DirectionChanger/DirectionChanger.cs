using System;
using UnityEngine;

namespace Enemy
{
    public class DirectionChanger : MonoBehaviour
    {
        public event Action<Vector3> DirectionChanged;

        public event Action<GameObject> OutSight;

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("BorderDown"))
            {
                transform.parent.gameObject.SetActive(false);
                OutSight?.Invoke(transform.parent.gameObject);
            }
        }

        protected void InvokeDirectionChanged(Vector3 newDirection)
        {
            DirectionChanged?.Invoke(newDirection);
        }
    }
}