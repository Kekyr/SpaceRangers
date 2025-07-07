using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Game
{
    public class ScreenAdjuster : MonoBehaviour
    {
        private PostProcessProfile _postProcessProfile;
        private Canvas _canvas;
        private Camera _mainCamera;
        private RawImage _backgroundImage;

        private float _width;
        private float _height;
        private int _pixelsPerUnit;

        private RectTransform _canvasRectTransform;

        public Action ResolutionChanged;

        private void Awake()
        {
            _canvasRectTransform = _canvas.GetComponent<RectTransform>();
            _pixelsPerUnit = _mainCamera.GetComponent<PixelPerfectCamera>().assetsPPU;

            _width = _canvasRectTransform.rect.width;
            _height = _canvasRectTransform.rect.height;
        }

        private void FixedUpdate()
        {
            if (_width != _canvasRectTransform.rect.width || _height != _canvasRectTransform.rect.height)
            {
                _width = _canvasRectTransform.rect.width;
                _height = _canvasRectTransform.rect.height;
                ChangeBackground();
                ChangeEffect();
                ResolutionChanged?.Invoke();
            }
        }

        public void Init(Canvas canvas, Camera mainCamera, RawImage backgroundImage,
            PostProcessProfile postProcessProfile)
        {
            _canvas = canvas;
            _mainCamera = mainCamera;
            _backgroundImage = backgroundImage;
            _postProcessProfile = postProcessProfile;
        }

        public void Clamp(Transform gameObjectTransform, Collider2D gameObjectCollider)
        {
            Check(gameObjectTransform, gameObjectCollider.bounds.min, false);
            Check(gameObjectTransform, gameObjectCollider.bounds.max, true);
        }

        public void ChangeBackground()
        {
            float newWidth = (_canvasRectTransform.rect.width / _pixelsPerUnit) / 10;
            float newHeight = (_canvasRectTransform.rect.height / _pixelsPerUnit) / 10;

            Vector2 newSize = new Vector2 (newWidth, newHeight);

            _backgroundImage.uvRect = new Rect(_backgroundImage.uvRect.position, newSize);
        }

        public void ChangeEffect()
        {
            float maxValue = 0.004f;
            float oneFourth = 5;
            float newValue;

            float unitsCount = _canvasRectTransform.rect.width / _pixelsPerUnit;
            float modifier = unitsCount / oneFourth;

            newValue = maxValue / modifier;

            _postProcessProfile.TryGetSettings(out CRT crtSetting);
            crtSetting.intensity.value = newValue;
        }

        private void Check(Transform gameObjectTransform, Vector3 position, bool isMax)
        {
            Vector3 newScreenPosition;

            Vector3 direction = gameObjectTransform.position - position;

            Vector3 screenPosition = _mainCamera.WorldToScreenPoint(position);

            if (isMax == true)
            {
                newScreenPosition = new Vector3(
                    screenPosition.x <= _canvas.pixelRect.max.x ? screenPosition.x : _canvas.pixelRect.max.x,
                    screenPosition.y <= _canvas.pixelRect.max.y ? screenPosition.y : _canvas.pixelRect.max.y,
                    _mainCamera.nearClipPlane);
            }
            else
            {
                newScreenPosition = new Vector3(
                    screenPosition.x >= _canvas.pixelRect.min.x ? screenPosition.x : _canvas.pixelRect.min.x,
                    screenPosition.y >= _canvas.pixelRect.min.y ? screenPosition.y : _canvas.pixelRect.min.y,
                    _mainCamera.nearClipPlane);
            }

            Vector3 newWorldPosition = _mainCamera.ScreenToWorldPoint(newScreenPosition);
            newWorldPosition.z = 0f;

            Vector3 newGameObjectPosition = newWorldPosition + direction;
            gameObjectTransform.position = newGameObjectPosition;
        }
    }
}