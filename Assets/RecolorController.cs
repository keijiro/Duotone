using UnityEngine;
using UnityEngine.Rendering;

namespace RecolorURP {

[ExecuteInEditMode]
public sealed class RecolorController : MonoBehaviour
{
    #region Public properties

    [field:SerializeField]
    public Gradient FillGradient { get; set; } = DefaultGradient;

    [field:SerializeField, HideInInspector]
    public Shader Shader { get; set; }

    public Material Material => UpdateMaterial();

    #endregion

    #region Class constants

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

    #endregion

    #region Controller implementation

    Material _material;

    public Material UpdateMaterial()
    {
        _material = _material ?? CoreUtils.CreateEngineMaterial(Shader);

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
