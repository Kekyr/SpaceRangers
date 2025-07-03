using System.Collections;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class Gun : AutoGun
    {
        private readonly string _shootTrigger = "Shoot";

        private Animator _animator;

        protected override void Awake()
        {
            _animator = GetComponent<Animator>();
            base.Awake();
        }

        protected override void OnEnable()
        {
            _animator.SetTrigger(_shootTrigger);
        }

        protected override void ShootBullet()
        {
            base.ShootBullet();
            StartCoroutine(Prepare());
        }

        protected override IEnumerator Prepare()
        {
            yield return Wait;
            _animator.SetTrigger(_shootTrigger);
        }
    }
}