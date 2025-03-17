using System;
using DG.Tweening;
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

        private float _volume;

        private void Awake()
        {
            if (_data == null)
            {
                throw new ArgumentNullException(nameof(_data));
            }

            _timer.Ends += OnEnds;
            _timer.Ended += Pause;
            _audioSource = GetComponent<AudioSource>();

            Play(_data.GetRandomClip());
        }

        private void OnDisable()
        {
            _timer.Ends -= OnEnds;
            _timer.Ended += Pause;
        }

        public void Init(Timer timer)
        {
            _timer = timer;
            enabled = true;
        }

        public void Pause()
        {
            _audioSource.volume = 0f;
        }

        public void Continue()
        {
            _audioSource.volume = _volume;
        }

        private void Play(AudioSO audio)
        {
            if (audio == null)
            {
                return;
            }

            _audioSource.clip = audio.Clip;
            _volume = audio.Volume == 0 ? _audioSource.volume : audio.Volume;
            _audioSource.volume = _volume;
            _audioSource.Play();
        }

        private void OnEnds()
        {
            _audioSource.DOPitch(_endPitch, _timer.EndTime);
        }
    }
}