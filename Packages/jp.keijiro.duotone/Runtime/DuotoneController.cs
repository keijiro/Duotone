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

    static Vector4 ToVector(Color color, float alpha)
      => new Vector4(color.r, color.g, color.b, alpha);

    public Material UpdateMaterial()
    {
        if (_material == null)
        {
            _token = new ShaderToken(Shader);
            _material = CoreUtils.CreateEngineMaterial(Shader);
        }

        // Color keys
        _material.SetVector(_token.ColorKey0, ToVector(BlackColor, BlackLevel));
        _material.SetVector(_token.ColorKey1, ToVector(LowColor, SplitLevel));
        _material.SetVector(_token.ColorKey2, ToVector(HighColor, WhiteLevel));
        _material.SetVector(_token.ColorKey3, ToVector(WhiteColor, 1));

        // Dithering
        _material.SetKeyword(_token.Bayer2x2, DitherType == DitherType.Bayer2x2);
        _material.SetKeyword(_token.Bayer3x3, DitherType == DitherType.Bayer3x3);
        _material.SetKeyword(_token.Bayer4x4, DitherType == DitherType.Bayer4x4);
        _material.SetKeyword(_token.Bayer8x8, DitherType == DitherType.Bayer8x8);
        _material.SetFloat(_token.DitherStrength, DitherStrength);

        // Edge detection
        _material.SetKeyword(_token.EnableEdge, EdgeColor.a > 0);
        _material.SetColor(_token.EdgeColor, EdgeColor);

        return _material;
    }

    #endregion
}

} // namespace Duotone
