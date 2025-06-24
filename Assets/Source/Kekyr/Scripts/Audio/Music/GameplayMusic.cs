using System;
using Enemy;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameplayMusic : Music
    {
        [SerializeField] private MusicSO _boss;

        private EnemySpawners _enemySpawners;

        protected override void Start()
        {
            if (_boss == null)
            {
                throw new ArgumentNullException(nameof(_boss));
            }

            base.Start();
            _enemySpawners.Spawned += OnSpawned;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _enemySpawners.Spawned -= OnSpawned;
        }

        public void Init(EnemySpawners enemySpawners)
        {
            _enemySpawners = enemySpawners;
        }

        public void Init(EnemyShip boss)
        {
            boss.Destroyed += OnDestroyed;
        }

        private void OnDestroyed()
        {
            Stop();
        }

        private void OnSpawned()
        {
            Stop();
            Play(_boss.GetRandomClip());
        }
    }
}