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

    ShaderToken _token;
    Material _material;

    static Vector4 ToColorKey(Color color, float alpha)
      => new Vector4(color.r, color.g, color.b, alpha);

    public Material UpdateMaterial()
    {
        if (_material == null)
        {
            _token = new ShaderToken(Shader);
            _material = CoreUtils.CreateEngineMaterial(Shader);
        }

        // Color keys
        _material.SetVector(_token.ColorKey0, ToColorKey(BlackColor, BlackLevel));
        _material.SetVector(_token.ColorKey1, ToColorKey(LowColor, SplitLevel));
        _material.SetVector(_token.ColorKey2, ToColorKey(HighColor, WhiteLevel));
        _material.SetVector(_token.ColorKey3, ToColorKey(WhiteColor, 1));

        // Dithering
        _material.SetKeyword(_token.Bayer2x2, DitherType == DitherType.Bayer2x2);
        _material.SetKeyword(_token.Bayer3x3, DitherType == DitherType.Bayer3x3);
        _material.SetKeyword(_token.Bayer4x4, DitherType == DitherType.Bayer4x4);
        _material.SetKeyword(_token.Bayer8x8, DitherType == DitherType.Bayer8x8);
        _material.SetFloat(_token.DitherStrength, DitherStrength);

        // Extended mode (Duotone Surface source mode)
        _material.SetKeyword(_token.Extended, SourceMode == SourceMode.DuotoneSurface);
        _material.SetColor(_token.ContourColor, ContourColor);

        return _material;
    }

    #endregion
}

} // namespace Duotone
