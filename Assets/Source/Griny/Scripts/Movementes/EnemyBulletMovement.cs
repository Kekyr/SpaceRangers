using ShipBase;
using UnityEngine;
using WordGame;

namespace Enemy
{
    [RequireComponent(typeof(Bullet))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBulletMovement : EnemyMovement
    {
        private readonly string _destructionTrigger = "Destruct";
        //private readonly string _leftBorder = "left";
        //private readonly string _rightBorder = "right";

        private Vector3 _pointInterection;

        private Bullet _bullet;
        private Animator _animator;
        private Collider2D _collider;

        /*private Camera _camera;
        private Canvas _canvas;*/

        protected override void Awake()
        {
            base.Awake();

            _bullet = GetComponent<Bullet>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            //Debug.Log(Camera);
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        /*public new void Init(Camera camera, Canvas canvas)
        {
            _camera = camera;
            _canvas = canvas;
        }*/


        protected override void InteractWithWorld(float speed)
        {
            //Debug.Log(Camera);

            _pointInterection = Camera.WorldToScreenPoint(PointInterectionUp.position);

            if(_pointInterection.y <= 0 || _pointInterection.x >= Canvas.pixelRect.size.x || _pointInterection.x <= 0)
            {
                speed = 0;
                gameObject.SetActive(false);
            }
        }


        protected override Vector2 GetVelocity(float speed)
        {
            return _bullet.Direction * speed * Time.deltaTime;
        }

        protected override void CollideShip(Collider2D collider, float speed)
        {
            //base.CollideShip(collider, speed);

            //if (collider.gameObject.TryGetComponent(out BackgroundBorder backgruondBorder))
            //{
            //    string borderName = backgruondBorder.GetName();

            //    if (borderName == _leftBorder || borderName == _rightBorder)
            //    {
            //        gameObject.SetActive(false);
            //    }
            //}

            if (collider.gameObject.CompareTag("Player"))
            {
                _collider.enabled = false;
                Stop();
                _animator.SetTrigger(_destructionTrigger);
            }
        }
    }
}