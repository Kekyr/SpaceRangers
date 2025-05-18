using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(CRTRenderer), PostProcessEvent.AfterStack, "Custom/CRT")]
public sealed class CRT : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("CRT effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
}
public sealed class CRTRenderer : PostProcessEffectRenderer<CRT>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/CRT"));
        sheet.properties.SetFloat("_Blend", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
