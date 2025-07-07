using UnityEngine;

namespace Background
{
    [CreateAssetMenu(fileName = "new BackgroundSO", menuName = "BackgroundSO/Create new BackgroundSO")]
    public class BackgroundSO : ScriptableObject
    {
        [SerializeField] private Texture[] _textures;
        [SerializeField] private int _currentTextureIndex;

        public Texture CurrentTexture => _textures[_currentTextureIndex];

        public void Init(int currentTextureIndex)
        {
            _currentTextureIndex = currentTextureIndex;
        }

        public void SetCurrent(int index)
        {
            _currentTextureIndex = index;
        }
    }
}