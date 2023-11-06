void DuotoneSamplePoints_float
  (float2 UV, float Width, float Height,
   out float2 UV1, out float2 UV2, out float2 UV3)
{
    UV1 = UV + float2(1 / Width, 1 / Height);
    UV2 = float2(UV.x, UV1.y);
    UV3 = float2(UV1.x, UV.y);
}

void DuotoneMain_float
  (float3 C0, float3 C1, float3 C2, float3 C3,
   float4 EdgeColor, float2 EdgeThreshold, float FillOpacity,
   float4 ColorKey0, float4 ColorKey1, float4 ColorKey2, float4 ColorKey3,
   float4 ColorKey4, float4 ColorKey5, float4 ColorKey6, float4 ColorKey7,
   out float3 Output)
{
    // Roberts cross operator
    float3 g1 = C1.yzy - C0.yzy;
    float3 g2 = C3.yzy - C2.yzy;
    float g = sqrt(dot(g1, g1) + dot(g2, g2)) * 10;

    float3 fill = ColorKey0.rgb;
    float lum = Luminance(C0.rrr);

    fill = lerp(fill, ColorKey1.rgb, saturate((lum - ColorKey0.w) / (ColorKey1.w - ColorKey0.w)));
    fill = lerp(fill, ColorKey2.rgb, saturate((lum - ColorKey1.w) / (ColorKey2.w - ColorKey1.w)));
    fill = lerp(fill, ColorKey3.rgb, saturate((lum - ColorKey2.w) / (ColorKey3.w - ColorKey2.w)));
    fill = lerp(fill, ColorKey4.rgb, saturate((lum - ColorKey3.w) / (ColorKey4.w - ColorKey3.w)));
    fill = lerp(fill, ColorKey5.rgb, saturate((lum - ColorKey4.w) / (ColorKey5.w - ColorKey4.w)));
    fill = lerp(fill, ColorKey6.rgb, saturate((lum - ColorKey5.w) / (ColorKey6.w - ColorKey5.w)));
    fill = lerp(fill, ColorKey7.rgb, saturate((lum - ColorKey6.w) / (ColorKey7.w - ColorKey6.w)));

    float edge = smoothstep(EdgeThreshold.x, EdgeThreshold.y, g);
    float3 cb = lerp(C0, fill, FillOpacity);
    Output = lerp(cb, EdgeColor.rgb, edge * EdgeColor.a);
}
