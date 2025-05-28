using System;
using DG.Tweening;
using Enemy;
using LevelEnemy;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Music : MonoBehaviour
    {
        private readonly float _endPitch = 1.5f;

        [SerializeField] private MusicSO _data;

        private AudioSource _audioSource;
        private Timer _timer;
        private AudioSettingSO _setting;
        private AudioButton _button;

        private float _volume;

        private void Start()
        {
            if (_data == null)
            {
                throw new ArgumentNullException(nameof(_data));
            }
            
            _timer.Ended += OnEnded;
            _button.Switched += OnSwitched;

            _audioSource = GetComponent<AudioSource>();
            Play(_data.GetRandomClip());
        }

        private void OnDestroy()
        {
            _timer.Ended += OnEnded;
            _button.Switched -= OnSwitched;
        }

        public void Init(Timer timer, AudioSettingSO setting, AudioButton button)
        {
            _timer = timer;
            _setting = setting;
            _button = button;
            enabled = true;
        }

        public void Init(EnemyShip boss)
        {
            boss.Destroyed += OnDestroyed;
        }

        public void Pause()
        {
            _audioSource.volume = 0f;
        }

        public void Continue()
        {
            if (_setting.IsOn == false)
            {
                return;
            }

            _audioSource.volume = _volume;
        }

        private void Play(AudioSO audio)
        {
            if (_setting.IsOn == false)
            {
                return;
            }

            if (audio == null)
            {
                return;
            }

            _audioSource.clip = audio.Clip;
            _volume = audio.Volume == 0 ? _audioSource.volume : audio.Volume;
            _audioSource.volume = _volume;
            _audioSource.Play();
        }

        private void OnSwitched()
        {
            _audioSource.Stop();
            Play(_data.GetRandomClip());
        }

        private void OnEnded()
        {
            _audioSource.Stop();
        }

        private void OnDestroyed()
        {
            _audioSource.Stop();
        }
    }
}