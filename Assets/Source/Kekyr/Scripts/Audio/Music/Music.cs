using System;
using Audio;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class Music : MonoBehaviour
    {
        [SerializeField] private MusicSO _data;

        private AudioSource _audioSource;

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
            _audioSource.volume = audio.Volume == 0 ? _audioSource.volume : audio.Volume;
            _audioSource.Play();
        }
    }
}