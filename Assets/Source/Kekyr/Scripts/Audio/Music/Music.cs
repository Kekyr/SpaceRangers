using System;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Music : MonoBehaviour
    {
        [SerializeField] private MusicSO _data;

        private AudioSource _audioSource;
        private float _volume;

        private void Awake()
        {
            if (_data == null)
            {
                throw new ArgumentNullException(nameof(_data));
            }

            _audioSource = GetComponent<AudioSource>();

            Play(_data.GetRandomClip());
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

        public void Pause()
        {
            _audioSource.volume = 0f;
        }

        public void Continue()
        {
            _audioSource.volume = _volume;
        }
    }
}