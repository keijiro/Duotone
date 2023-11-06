using UnityEngine;
using UnityEngine.Rendering;

namespace RecolorURP {

[ExecuteInEditMode]
public sealed class RecolorController : MonoBehaviour
{
    #region Public properties

    [field:SerializeField]
    public Color EdgeColor { get; set; } = Color.black;

    [field:SerializeField, Range(0, 1)]
    public float EdgeThreshold { get; set; } = 0.5f;

    [field:SerializeField, Range(0, 1)]
    public float EdgeContrast { get; set; } = 0.5f;

    [field:SerializeField]
    public Gradient FillGradient { get; set; } = DefaultGradient;

    [field:SerializeField, Range(0, 1)]
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
    }

    static Gradient DefaultGradient
      => new Gradient
        { colorKeys = new [] { new GradientColorKey(Color.blue, 0),
                               new GradientColorKey(Color.red , 1) },
          alphaKeys = new [] { new GradientAlphaKey(1, 0),
                               new GradientAlphaKey(1, 1) } };

    static readonly int[] _colorKeyPropertyIDs
      = new [] { Shader.PropertyToID("_ColorKey0"),
                 Shader.PropertyToID("_ColorKey1"),
                 Shader.PropertyToID("_ColorKey2"),
                 Shader.PropertyToID("_ColorKey3"),
                 Shader.PropertyToID("_ColorKey4"),
                 Shader.PropertyToID("_ColorKey5"),
                 Shader.PropertyToID("_ColorKey6"),
                 Shader.PropertyToID("_ColorKey7") };

    #endregion

    #region MonoBehaviour implementation

    void OnDestroy()
      => CoreUtils.Destroy(_material);

    void Update() {}

    #endregion

    #region Controller implementation

    Material _material;

    public Material UpdateMaterial()
    {
        _material = _material ?? CoreUtils.CreateEngineMaterial(Shader);

        var edgeThresh = new Vector2(EdgeThreshold, EdgeThreshold + 1.01f - EdgeContrast);

        _material.SetColor(ShaderIDs.EdgeColor, EdgeColor);
        _material.SetVector(ShaderIDs.EdgeThreshold, edgeThresh);
        _material.SetFloat(ShaderIDs.FillOpacity, FillOpacity);

        var keys = FillGradient.colorKeys;

        for (var i = 0; i < 8; i++)
        {
            var key = keys[Mathf.Min(i, keys.Length - 1)];
            var color = key.color.linear;
            var vector = new Vector4(color.r, color.g, color.b, key.time);
            _material.SetVector(_colorKeyPropertyIDs[i], vector);
        }

        return _material;
    }

    #endregion
}

} // namespace RecolorURP
