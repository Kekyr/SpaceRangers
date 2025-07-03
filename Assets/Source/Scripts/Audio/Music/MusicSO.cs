using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "new MusicSO", menuName = "MusicSO/Create new MusicSO")]
    public class MusicSO : ScriptableObject
    {
        [SerializeField] private List<AudioSO> _tracks = new List<AudioSO>();

        private int _currentTrackIndex;
        private int _nextTrackIndex;

        public AudioSO GetRandomClip()
        {
            _nextTrackIndex = Random.Range(0, _tracks.Count);

            _currentTrackIndex = _nextTrackIndex;

            return _tracks[_nextTrackIndex];
        }
    }
}