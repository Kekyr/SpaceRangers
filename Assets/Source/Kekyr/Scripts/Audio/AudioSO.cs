using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "new AudioSO", menuName = "AudioSO/Create new AudioSO")]
    public class AudioSO : ScriptableObject
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volume;

        public AudioClip Clip => _clip;
        public float Volume => _volume;
    }
}
