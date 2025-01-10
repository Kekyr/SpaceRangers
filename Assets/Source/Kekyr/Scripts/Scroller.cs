using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Scroller : MonoBehaviour
{
    private readonly float _yModifier = 0.03f;

    private RawImage _rawImage;
    private Vector2 _positionModifier;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();

        _positionModifier = new Vector2(_rawImage.uvRect.x, _yModifier);
    }

    private void OnGUI()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + _positionModifier * Time.deltaTime,
            _rawImage.uvRect.size);
    }
}