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
   out float3 Output)
{
    // Roberts cross operator
    float3 g1 = C1.yzy - C0.yzy;
    float3 g2 = C3.yzy - C2.yzy;
    float g = sqrt(dot(g1, g1) + dot(g2, g2)) * 10;

    float3 fill = ColorKey0.rgb;
    float lum = Luminance(C0.rrr);

    fill = lum > ColorKey0.w ? ColorKey1.rgb : fill;
    fill = lum > ColorKey1.w ? ColorKey2.rgb : fill;
    fill = lum > ColorKey2.w ? ColorKey3.rgb : fill;

    float edge = smoothstep(EdgeThreshold.x, EdgeThreshold.y, g);
    float3 cb = lerp(C0, fill, FillOpacity);
    Output = lerp(cb, EdgeColor.rgb, edge * EdgeColor.a);
}
