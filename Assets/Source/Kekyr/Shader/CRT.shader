Shader "Hidden/Custom/CRT"
{
    HLSLINCLUDE
    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
    TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

    float _Blend;

    float4 Frag(VaryingsDefault i) : SV_Target
    {
        float red = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x-_Blend,i.texcoord.y)).r;
        float green = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x,i.texcoord.y)).g;
        float blue = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x+_Blend,i.texcoord.y)).b;
        
        float4 color = float4(red, green, blue, 1);

        return color;
    }
    ENDHLSL
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment Frag
            ENDHLSL
        }
    }
}