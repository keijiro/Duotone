using UnityEngine;
using UnityEngine.Rendering;

namespace DuotoneURP {

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

    [field:SerializeField, HideInInspector]
    public Shader Shader { get; set; }

    public Material Material => UpdateMaterial();

    #endregion

    #region Class constants

    static class ShaderIDs
    {
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
      => CoreUtils.Destroy(_material);

    void Update() {}

    #endregion

    #region Controller implementation

    Material _material;

    static Vector4 ToVector(Color color, float alpha)
      => new Vector4(color.r, color.g, color.b, alpha);

    public Material UpdateMaterial()
    {
        _material = _material ?? CoreUtils.CreateEngineMaterial(Shader);

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

        return _material;
    }

    #endregion
}

} // namespace DuotoneURP
