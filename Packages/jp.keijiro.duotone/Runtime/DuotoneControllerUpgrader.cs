using UnityEngine;

namespace Duotone {

public sealed partial class DuotoneController
{
    Shader GetShader()
    {
#if UNITY_EDITOR
        if (Shader == null)
        {
            UnityEditor.Undo.RecordObject(this, "Upgrade Duotone Shader");
            Shader = Shader.Find("Hidden/DuotoneProcess");
            Debug.Log("[Duotone] Upgraded shader reference.", this);
        }
#endif
        return Shader;
    }
}

} // namespace Duotone
