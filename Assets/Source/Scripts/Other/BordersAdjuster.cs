using System;
using UnityEngine;
using UnityEngine.U2D;

namespace Game
{
    public class BordersAdjuster : MonoBehaviour
    {
        private readonly float _torpedoHeightInPercent = 80;
        private readonly float _autoGunsSizeYOffset = 1.5f;
        private readonly float _autoGunsYOffset = 0.7f;

        [SerializeField] private BoxCollider2D _up;
        [SerializeField] private BoxCollider2D _left;
        [SerializeField] private BoxCollider2D _down;
        [SerializeField] private BoxCollider2D _right;
        [SerializeField] private BoxCollider2D _fighterDown;
        [SerializeField] private BoxCollider2D _torpedo;
        [SerializeField] private BoxCollider2D _autoGunsZone;

        private ScreenAdjuster _screenAdjuster;
        private Camera _mainCamera;
        private Canvas _canvas;

        private int _pixelsPerUnit;

        private void Start()
        {
            if (_up == null)
            {
                throw new ArgumentNullException(nameof(_up));
            }

            if (_left == null)
            {
                throw new ArgumentNullException(nameof(_left));
            }

            if (_down == null)
            {
                throw new ArgumentNullException(nameof(_down));
            }

            if (_right == null)
            {
                throw new ArgumentNullException(nameof(_right));
            }

            if (_fighterDown == null)
            {
                throw new ArgumentNullException(nameof(_right));
            }

            if (_torpedo == null)
            {
                throw new ArgumentNullException(nameof(_torpedo));
            }

            if (_autoGunsZone == null)
            {
                throw new ArgumentNullException(nameof(_autoGunsZone));
            }

            _pixelsPerUnit = _mainCamera.GetComponent<PixelPerfectCamera>().assetsPPU;

            _screenAdjuster.ResolutionChanged += OnResolutionChanged;
        }

        private void OnDisable()
        {
            _screenAdjuster.ResolutionChanged -= OnResolutionChanged;
        }

        public void Init(Canvas canvas, Camera mainCamera, ScreenAdjuster screenAdjuster)
        {
            _canvas = canvas;
            _mainCamera = mainCamera;
            _screenAdjuster = screenAdjuster;
            enabled = true;
        }

        public void OnResolutionChanged()
        {
            int modifier = 2;

            RectTransform rectTransform = _canvas.GetComponent<RectTransform>();

            float sizeX = ((rectTransform.rect.width / _pixelsPerUnit) * modifier) / 10;
            float sizeY = (rectTransform.rect.height / _pixelsPerUnit + modifier) / 10;

            float autoGunsZoneSizeX = rectTransform.rect.width / _pixelsPerUnit;
            float autoGunsZoneSizeY = rectTransform.rect.height / _pixelsPerUnit - _autoGunsSizeYOffset;

            float torpedoHeight = (_canvas.pixelRect.height / 100) * _torpedoHeightInPercent;

            _up.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.max.y));
            _left.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.min.x, _canvas.pixelRect.center.y));
            _down.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.min.y));
            _right.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.max.x, _canvas.pixelRect.center.y));
            
            _fighterDown.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.center.y));
            _torpedo.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, torpedoHeight));
            
            _autoGunsZone.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(_canvas.pixelRect.center.x, _canvas.pixelRect.center.y));
            _autoGunsZone.transform.position = new Vector3(_autoGunsZone.transform.position.x, _autoGunsZone.transform.position.y + _autoGunsYOffset);

            _up.size = new Vector2(sizeX, _up.size.y);
            _down.size = new Vector2(sizeX, _down.size.y);

            _fighterDown.size = new Vector2(sizeX, _fighterDown.size.y);
            _torpedo.size = new Vector2(sizeX, _torpedo.size.y);

            _left.size = new Vector2(_left.size.x, sizeY);
            _right.size = new Vector2(_right.size.x, sizeY);

            _autoGunsZone.size = new Vector2(autoGunsZoneSizeX, autoGunsZoneSizeY);
        }
    }
}