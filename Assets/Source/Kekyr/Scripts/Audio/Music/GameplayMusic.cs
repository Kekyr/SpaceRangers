using Enemy;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class GameplayMusic : Music
    {
        private Timer _timer;
        private AudioButton _button;

        protected override void Start()
        {
            base.Start();
            _timer.Ended += OnEnded;
            _button.Switched += OnSwitched;
        }

        private void OnDestroy()
        {
            _timer.Ended += OnEnded;
            _button.Switched -= OnSwitched;
        }

        public void Init(Timer timer, AudioButton button)
        {
            _timer = timer;
            _button = button;
        }

        public void Init(EnemyShip boss)
        {
            boss.Destroyed += OnDestroyed;
        }

        private void OnSwitched()
        {
            Pause();
            Play();
        }

        private void OnEnded()
        {
            Pause();
        }

        private void OnDestroyed()
        {
            Pause();
        }
    }
}