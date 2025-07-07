using UnityEngine;
using UnityEngine.UI;

namespace Background
{
    [RequireComponent(typeof(RawImage))]
    public class BackgroundMovement : MonoBehaviour
    {
        private readonly float _yModifier = 0.03f;

        private RawImage _rawImage;

        private Vector2 _positionModifier;

        private void Start()
        {
            _rawImage = GetComponent<RawImage>();
            _positionModifier = new Vector2(_rawImage.uvRect.x, _yModifier);
        }

        private void LateUpdate()
        {
            _rawImage.uvRect = new Rect(_rawImage.uvRect.position + _positionModifier * Time.deltaTime, _rawImage.uvRect.size);
        }
    }
}