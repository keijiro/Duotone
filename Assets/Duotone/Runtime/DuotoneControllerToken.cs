using UnityEngine;
using UnityEngine.Rendering;

namespace DuotoneURP {

public sealed partial class DuotoneController
{
    struct ShaderToken
    {
        public int DitherStrength;
        public int EdgeColor;
        public int ColorKey0;
        public int ColorKey1;
        public int ColorKey2;
        public int ColorKey3;

        public LocalKeyword Bayer2x2;
        public LocalKeyword Bayer3x3;
        public LocalKeyword Bayer4x4;
        public LocalKeyword Bayer8x8;

        public ShaderToken(Shader shader)
        {
            DitherStrength = Shader.PropertyToID("_DitherStrength");
            EdgeColor = Shader.PropertyToID("_EdgeColor");
            ColorKey0 = Shader.PropertyToID("_ColorKey0");
            ColorKey1 = Shader.PropertyToID("_ColorKey1");
            ColorKey2 = Shader.PropertyToID("_ColorKey2");
            ColorKey3 = Shader.PropertyToID("_ColorKey3");

            Bayer2x2 = new LocalKeyword(shader, "_DITHERTYPE_BAYER2X2");
            Bayer3x3 = new LocalKeyword(shader, "_DITHERTYPE_BAYER3X3");
            Bayer4x4 = new LocalKeyword(shader, "_DITHERTYPE_BAYER4X4");
            Bayer8x8 = new LocalKeyword(shader, "_DITHERTYPE_BAYER8X8");
        }
    }
}

} // namespace DuotoneURP
