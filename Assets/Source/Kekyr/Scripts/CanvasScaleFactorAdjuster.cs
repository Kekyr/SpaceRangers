using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CanvasScaleFactorAdjuster : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    private CanvasScaler _canvasScaler;

    private void Start()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        AdjustScalingFactor();
    }

    private void LateUpdate()
    {
        AdjustScalingFactor();
    }

    private void AdjustScalingFactor()
    {
        _canvasScaler.scaleFactor = _mainCamera.GetComponent<PixelPerfectCamera>().pixelRatio;
    }
}