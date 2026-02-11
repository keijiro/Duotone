using UnityEngine;
using UnityEngine.Rendering;

namespace Duotone {

[ExecuteInEditMode]
public sealed partial class DuotoneController : MonoBehaviour
{
    #region MonoBehaviour implementation

    void OnDestroy()
      => CoreUtils.Destroy(_material);

    void OnDisable()
      => OnDestroy();

    void Update() {} // Just for providing the component enable switch.

    #endregion

    #region Controller implementation

    ShaderBindings _bindings;
    Material _material;

    static Vector4 ToColorKey(Color color, float alpha)
      => new Vector4(color.r, color.g, color.b, alpha);

    public Material UpdateMaterial()
    {
        if (_material == null)
        {
            var shader = GetShader();
            _bindings = new ShaderBindings(shader);
            _material = CoreUtils.CreateEngineMaterial(shader);
        }

        // Color keys
        _material.SetVector(_bindings.ColorKey0, ToColorKey(BlackColor, BlackLevel));
        _material.SetVector(_bindings.ColorKey1, ToColorKey(LowColor, SplitLevel));
        _material.SetVector(_bindings.ColorKey2, ToColorKey(HighColor, WhiteLevel));
        _material.SetVector(_bindings.ColorKey3, ToColorKey(WhiteColor, 1));

        // Dithering
        _material.SetKeyword(_bindings.Bayer2x2, DitherType == DitherType.Bayer2x2);
        _material.SetKeyword(_bindings.Bayer3x3, DitherType == DitherType.Bayer3x3);
        _material.SetKeyword(_bindings.Bayer4x4, DitherType == DitherType.Bayer4x4);
        _material.SetKeyword(_bindings.Bayer8x8, DitherType == DitherType.Bayer8x8);
        _material.SetFloat(_bindings.DitherStrength, DitherStrength);

        // Extended mode (Duotone Surface source mode)
        _material.SetKeyword(_bindings.Extended, SourceMode == SourceMode.DuotoneSurface);
        _material.SetColor(_bindings.ContourColor, ContourColor);

        return _material;
    }

    #endregion
}

} // namespace Duotone
