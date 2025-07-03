using System;
using UnityEngine;

namespace Audio
{
    public class Music : MonoBehaviour
    {
        [SerializeField] private MusicSO _data;

        private AudioSource _audioSource;
        private AudioButton _button;
        private AudioSettingSO _setting;

        private float _volume;

        protected virtual void Start()
        {
            if (_data == null)
            {
                throw new ArgumentNullException(nameof(_data));
            }

            _audioSource = GetComponent<AudioSource>();
            Play();

            _button.Switched += OnSwitched;
        }

        protected virtual void OnDestroy()
        {
            _button.Switched -= OnSwitched;
        }

        public void Init(AudioButton button, AudioSettingSO setting)
        {
            _button = button;
            _setting = setting;
            enabled = true;
        }

        public void Play()
        {
            AudioSO audio = _data.GetRandomClip();
            Play(audio);
        }

        public void Play(AudioSO audio)
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

        public void Stop()
        {
            _audioSource.Stop();
        }

        private void OnSwitched()
        {
            Stop();
            Play();
        }
    }
}