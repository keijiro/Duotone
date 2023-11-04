void RecolorSamplePoints_float
  (float2 UV, out float2 UV1, out float2 UV2, out float2 UV3)
{
    UV1 = UV + 0.01;
    UV2 = float2(UV.x, UV1.y);
    UV3 = float2(UV1.x, UV.y);
}

void RecolorMain_float
  (float3 C0, float3 C1, float3 C2, float3 C3, out float3 Output)
{
    // Roberts cross operator
    float3 g1 = C1 - C0.rgb;
    float3 g2 = C3 - C2;
    float g = sqrt(dot(g1, g1) + dot(g2, g2)) * 10;
    Output = g;
}
