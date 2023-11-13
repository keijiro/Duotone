#include "Packages/jp.keijiro.duotone/Shaders/DuotoneDither.hlsl"

void DuotoneSamplePoints_float
  (float2 UV, float Width, float Height,
   out float2 UV1, out float2 UV2, out float2 UV3)
{
    float stride = rcp(float2(Width, Height));
    UV1 = min(UV + stride, 1 - stride);
    UV2 = float2(UV.x, UV1.y);
    UV3 = float2(UV1.x, UV.y);
}

void DuotoneMain_float
  (float2 SPos, float Width, float Height,
   float3 C0, float3 C1, float3 C2, float3 C3,
   float4 ColorKey0, float4 ColorKey1, float4 ColorKey2, float4 ColorKey3,
   float4 EdgeColor, float DitherStrength,
   out float3 Output)
{
#if defined(_DUOTONE_ENABLE_EDGE)
    // Edge detection with Roberts cross operator
    float2 g1 = C1.yz - C0.yz;
    float2 g2 = C3.yz - C2.yz;
    float2 g = sqrt(g1 * g1 + g2 * g2);
    float edge1 = smoothstep(0.10, 0.20, g.x);
    float edge2 = smoothstep(0.03, 0.04, g.y);
    float edge = max(edge1, edge2);
    // Luminance embedded in the R channel
    float lum = C0.r;
#else
    // No edge detection
    float edge = 0;
    // Luminance from the color sample
    float lum = Luminance(C0.rgb);
#endif

    // Dithering
    float dither = DuotoneDither(SPos * float2(Width, Height));
    lum += (dither - 0.5) * DitherStrength;

    // Color key + dithering
    float3 fill = ColorKey0.rgb;
    fill = lum > ColorKey0.w ? ColorKey1.rgb : fill;
    fill = lum > ColorKey1.w ? ColorKey2.rgb : fill;
    fill = lum > ColorKey2.w ? ColorKey3.rgb : fill;

    // Composition
    Output = lerp(fill, EdgeColor.rgb, edge * EdgeColor.a);
}
