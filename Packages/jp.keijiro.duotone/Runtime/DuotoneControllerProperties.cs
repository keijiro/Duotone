using UnityEngine;

namespace Duotone {

public sealed partial class DuotoneController
{
    #region Public properties

    [field:SerializeField]
    public Color LowColor { get; set; } = Color.blue;

    [field:SerializeField]
    public Color HighColor { get; set; } = Color.red;

    [field:SerializeField]
    public float SplitLevel { get; set; } = 0.5f;

    [field:SerializeField]
    public Color BlackColor { get; set; } = Color.black;

    [field:SerializeField]
    public float BlackLevel { get; set; } = 0.05f;

    [field:SerializeField]
    public Color WhiteColor { get; set; } = Color.white;

    [field:SerializeField]
    public float WhiteLevel { get; set; } = 0.95f;

    [field:SerializeField]
    public Color EdgeColor { get; set; } = Color.black;

    [field:SerializeField]
    public DitherType DitherType { get; set; } = DitherType.Bayer3x3;

    [field:SerializeField, Range(0, 1)]
    public float DitherStrength { get; set; } = 0.25f;

    [field:SerializeField, HideInInspector]
    public Shader Shader { get; set; }

    public Material Material => UpdateMaterial();

    #endregion
}

} // namespace Duotone
