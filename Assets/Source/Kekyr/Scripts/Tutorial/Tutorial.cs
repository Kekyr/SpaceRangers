using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform[] _positions;

    private RectTransform _imageRectTransform;
    private TutorialSO _tutorialData;
    private int _currentPositionIndex;

    private void Awake()
    {
        if (_image == null)
        {
            throw new ArgumentNullException(nameof(_image));
        }

        if (_positions.Length == 0)
        {
            throw new ArgumentOutOfRangeException(nameof(_positions));
        }
        
        _imageRectTransform = _image.GetComponent<RectTransform>();
        _imageRectTransform.anchoredPosition = _positions[_currentPositionIndex].anchoredPosition;
    }
    
    private void OnMouseDown()
    {
        Debug.Log("Tutorial Completed!");
        _currentPositionIndex++;
        _imageRectTransform.anchoredPosition = _positions[_currentPositionIndex].anchoredPosition;
    }
}
