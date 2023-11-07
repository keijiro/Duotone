using UnityEngine;
using UnityEngine.Rendering;

namespace DuotoneURP {

public enum DitherType { Bayer2x2, Bayer3x3, Bayer4x4, Bayer8x8 }

[ExecuteInEditMode]
public sealed class DuotoneController : MonoBehaviour
{
    #region Public properties

    [field:SerializeField, Space]
    public Color EdgeColor { get; set; } = Color.black;

    [field:SerializeField, Range(0, 1)]
    public float EdgeThreshold { get; set; } = 0.5f;

    [field:SerializeField, Range(0, 1)]
    public float EdgeContrast { get; set; } = 0.5f;

    [field:SerializeField, Space]
    public Color LowColor { get; set; } = Color.blue;

    [field:SerializeField]
    public Color HighColor { get; set; } = Color.red;

    [field:SerializeField]
    public float SplitLevel { get; set; } = 0.5f;

    [field:SerializeField, Space]
    public Color BlackColor { get; set; } = Color.black;

    [field:SerializeField]
    public float BlackLevel { get; set; } = 0.05f;

    [field:SerializeField, Space]
    public Color WhiteColor { get; set; } = Color.white;

    [field:SerializeField]
    public float WhiteLevel { get; set; } = 0.95f;

    [field:SerializeField, Range(0, 1), Space]
    public float FillOpacity { get; set; } = 1;

    [field:SerializeField, Space]
    public DitherType DitherType { get; set; } = DitherType.Bayer3x3;

    [field:SerializeField, Range(0, 1)]
    public float DitherStrength { get; set; } = 0.25f;

    [field:SerializeField, HideInInspector]
    public Shader Shader { get; set; }

    public Material Material => UpdateMaterial();

    #endregion

    #region Class constants

    static class ShaderIDs
    {
        internal static readonly int DitherStrength = Shader.PropertyToID("_DitherStrength");
        internal static readonly int DitherTexture = Shader.PropertyToID("_DitherTexture");
        internal static readonly int EdgeColor = Shader.PropertyToID("_EdgeColor");
        internal static readonly int EdgeThreshold = Shader.PropertyToID("_EdgeThreshold");
        internal static readonly int FillOpacity = Shader.PropertyToID("_FillOpacity");
        internal static readonly int ColorKey0 = Shader.PropertyToID("_ColorKey0");
        internal static readonly int ColorKey1 = Shader.PropertyToID("_ColorKey1");
        internal static readonly int ColorKey2 = Shader.PropertyToID("_ColorKey2");
        internal static readonly int ColorKey3 = Shader.PropertyToID("_ColorKey3");
    }

    #endregion

    #region MonoBehaviour implementation

    void OnDestroy()
    {
        CoreUtils.Destroy(_material);
        CoreUtils.Destroy(_dither.texture);
    }

    void Update() {}

    #endregion

    #region Controller implementation

    Material _material;
    (DitherType type, Texture2D texture) _dither;

    static Vector4 ToVector(Color color, float alpha)
      => new Vector4(color.r, color.g, color.b, alpha);

    public Material UpdateMaterial()
    {
        _material = _material ?? CoreUtils.CreateEngineMaterial(Shader);

        if (DitherType != _dither.type || _dither.texture == null)
        {
            CoreUtils.Destroy(_dither.texture);
            _dither = (DitherType, GenerateDitherTexture(DitherType));
        }

        var edgeThresh = new Vector2(EdgeThreshold, EdgeThreshold + 1.01f - EdgeContrast);
        var color0 = ToVector(BlackColor, BlackLevel);
        var color1 = ToVector(LowColor, SplitLevel);
        var color2 = ToVector(HighColor, WhiteLevel);
        var color3 = ToVector(WhiteColor, 1);

        _material.SetColor(ShaderIDs.EdgeColor, EdgeColor);
        _material.SetVector(ShaderIDs.EdgeThreshold, edgeThresh);
        _material.SetFloat(ShaderIDs.FillOpacity, FillOpacity);
        _material.SetVector(ShaderIDs.ColorKey0, color0);
        _material.SetVector(ShaderIDs.ColorKey1, color1);
        _material.SetVector(ShaderIDs.ColorKey2, color2);
        _material.SetVector(ShaderIDs.ColorKey3, color3);
        _material.SetTexture(ShaderIDs.DitherTexture, _dither.texture);
        _material.SetFloat(ShaderIDs.DitherStrength, DitherStrength);

        return _material;
    }

    #endregion

    #region Dither texture generator

    static Texture2D GenerateDitherTexture(DitherType type)
    {
        if (type == DitherType.Bayer2x2)
        {
            var tex = new Texture2D(2, 2, TextureFormat.R8, false, true);
            tex.LoadRawTextureData(new byte [] {0, 170, 255, 85});
            tex.Apply();
            return tex;
        }

        if (type == DitherType.Bayer3x3)
        {
            var tex = new Texture2D(3, 3, TextureFormat.R8, false, true);
            tex.LoadRawTextureData(new byte [] {
                0, 223, 95, 191, 159, 63, 127, 31, 255
            });
            tex.Apply();
            return tex;
        }

        if (type == DitherType.Bayer4x4)
        {
            var tex = new Texture2D(4, 4, TextureFormat.R8, false, true);
            tex.LoadRawTextureData(new byte [] {
                0, 136, 34, 170, 204, 68, 238, 102,
                51, 187, 17, 153, 255, 119, 221, 85
            });
            tex.Apply();
            return tex;
        }

        if (type == DitherType.Bayer8x8)
        {
            var tex = new Texture2D(8, 8, TextureFormat.R8, false, true);
            tex.LoadRawTextureData(new byte [] {
                0, 194, 48, 242, 12, 206, 60, 255,
                129, 64, 178, 113, 141, 76, 190, 125,
                32, 226, 16, 210, 44, 238, 28, 222,
                161, 97, 145, 80, 174, 109, 157, 93,
                8, 202, 56, 250, 4, 198, 52, 246,
                137, 72, 186, 121, 133, 68, 182, 117,
                40, 234, 24, 218, 36, 230, 20, 214,
                170, 105, 153, 89, 165, 101, 149, 85
            });
            tex.Apply();
            return tex;
        }

        return null;
    }

    #endregion
}

} // namespace DuotoneURP
