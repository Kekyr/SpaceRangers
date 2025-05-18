using System;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class ModifiedCRT : MonoBehaviour
{
    [SerializeField] private Material _material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _material);
    }
}