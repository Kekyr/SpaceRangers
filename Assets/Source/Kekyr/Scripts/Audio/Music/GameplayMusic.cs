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
        private AudioButton _button;

        protected override void Start()
        {
            if (_boss == null)
            {
                throw new ArgumentNullException(nameof(_boss));
            }
            
            base.Start();
            _button.Switched += OnSwitched;
            _enemySpawners.Spawned += OnSpawned;
        }

        private void OnDestroy()
        {
            _button.Switched -= OnSwitched;
            _enemySpawners.Spawned -= OnSpawned;
        }

        public void Init(AudioButton button, EnemySpawners enemySpawners)
        {
            _button = button;
            _enemySpawners = enemySpawners;
        }

        public void Init(EnemyShip boss)
        {
            boss.Destroyed += OnDestroyed;
        }

        private void OnSwitched()
        {
            Stop();
            Play();
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