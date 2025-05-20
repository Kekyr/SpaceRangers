using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SFX : MonoBehaviour
    {
        private readonly int _minPitch = 1;
        private readonly int _maxPitch = 3;

        private AudioSource _audioSource;
        private AudioSettingSO _setting;

        private float _defaultVolume;
        private float _defaultPitch;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _defaultVolume = _audioSource.volume;
            _defaultPitch = _audioSource.pitch;
        }

        public void Init(AudioSettingSO setting)
        {
            _setting = setting;
            enabled = true;
        }

        public void Play(SFXSO sfx)
        {
            if (_setting.IsOn == false)
            {
                return;
            }
            
            AudioClip randomClip = sfx.GetRandomClip();

            if (randomClip == null)
            {
                return;
            }

            int randomPitch = Random.Range(_minPitch, _maxPitch);

            _audioSource.volume = sfx.Volume == 0 ? _defaultVolume : sfx.Volume;
            _audioSource.pitch = sfx.CanPitch == true ? randomPitch : _defaultPitch;
            
            _audioSource.PlayOneShot(randomClip);
        }
    }
}