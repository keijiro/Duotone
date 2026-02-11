Shader "Hidden/DuotoneProcess"
{
    Properties
    {
        _ColorKey0("", Color) = (0, 0, 0, 0.05)
        _ColorKey1("", Color) = (0, 0, 1, 0.5)
        _ColorKey2("", Color) = (1, 0, 0, 0.95)
        _ColorKey3("", Color) = (1, 1, 1, 1)
        _ContourColor("", Color) = (0, 0, 0, 1)
    }

HLSLINCLUDE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/Runtime/Utilities/Blit.hlsl"
#include "Packages/jp.keijiro.duotone/Shaders/DuotoneDither.hlsl"

float4 _ColorKey0;
float4 _ColorKey1;
float4 _ColorKey2;
float4 _ColorKey3;
float4 _ContourColor;
float _DitherStrength;

float4 Frag(Varyings input) : SV_Target
{
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

    float2 uv = input.texcoord;
    float width = _BlitTexture_TexelSize.z;
    float height = _BlitTexture_TexelSize.w;

    float2 stride = rcp(float2(width, height));
    float2 uv1 = min(uv + stride, 1 - stride);
    float2 uv2 = float2(uv.x, uv1.y);
    float2 uv3 = float2(uv1.x, uv.y);

    float3 c0 = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv).rgb;
    float3 c1 = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv1).rgb;
    float3 c2 = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv2).rgb;
    float3 c3 = SAMPLE_TEXTURE2D_X(_BlitTexture, sampler_LinearClamp, uv3).rgb;

    #if defined(_DUOTONE_EXTENDED)
    float2 g1 = c1.yz - c0.yz;
    float2 g2 = c3.yz - c2.yz;
    float2 g = sqrt(g1 * g1 + g2 * g2);
    float edge1 = smoothstep(0.10, 0.20, g.x);
    float edge2 = smoothstep(0.03, 0.04, g.y);
    float edge = max(edge1, edge2);
    float lum = c0.r;
    #else
    float edge = 0;
    float lum = Luminance(c0.rgb);
    #endif

    float dither = DuotoneDither(uv * float2(width, height));
    lum += (dither - 0.5) * _DitherStrength;

    float3 fill = _ColorKey0.rgb;
    fill = lum > _ColorKey0.w ? _ColorKey1.rgb : fill;
    fill = lum > _ColorKey1.w ? _ColorKey2.rgb : fill;
    fill = lum > _ColorKey2.w ? _ColorKey3.rgb : fill;

    float3 output = lerp(fill, _ContourColor.rgb, edge * _ContourColor.a);
    return float4(output, 1);
}

ENDHLSL

    SubShader
    {
        Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" }
        Pass
        {
            Name "Duotone"
            ZTest Always ZWrite Off Cull Off
            HLSLPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag
            #pragma shader_feature_local _DUOTONE_EXTENDED
            #pragma shader_feature_local _DUOTONE_BAYER2X2 _DUOTONE_BAYER3X3 _DUOTONE_BAYER4X4 _DUOTONE_BAYER8X8
            ENDHLSL
        }
    }
}
