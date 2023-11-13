using UnityEngine;
using UnityEditor;

namespace Duotone.Editor {

[CustomEditor(typeof(DuotoneController))]
sealed class DuotoneControllerEditor : UnityEditor.Editor
{
    #region Private members

    #pragma warning disable CS0649

    AutoProperty SourceMode;
    AutoProperty LowColor;
    AutoProperty HighColor;
    AutoProperty SplitLevel;
    AutoProperty BlackColor;
    AutoProperty BlackLevel;
    AutoProperty WhiteColor;
    AutoProperty WhiteLevel;
    AutoProperty ContourColor;
    AutoProperty DitherType;
    AutoProperty DitherStrength;

    Label LabelBasic = "Luminance mode only supports color remapping.";
    
    Label LabelAdvanced = "Duotone Surface mode supports contour lines " +
      "but requires using the Duotone Surface shader to draw scene objects.";

    #pragma warning restore CS0649

    #endregion

    #region Editor implementation

    void OnEnable() => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var advanced = SourceMode.Target.enumValueIndex
                         == (int)Duotone.SourceMode.DuotoneSurface;

        EditorGUILayout.PropertyField(SourceMode);
        EditorGUILayout.HelpBox(advanced ? LabelAdvanced : LabelBasic);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(LowColor);
        EditorGUILayout.PropertyField(HighColor);
        EditorGUILayout.PropertyField(SplitLevel);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(BlackColor);
        EditorGUILayout.PropertyField(BlackLevel);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(WhiteColor);
        EditorGUILayout.PropertyField(WhiteLevel);
        EditorGUILayout.Space();

        if (advanced)
        {
            EditorGUILayout.PropertyField(ContourColor);
            EditorGUILayout.Space();
        }

        EditorGUILayout.PropertyField(DitherType);
        EditorGUILayout.PropertyField(DitherStrength);

        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}

} // namespace Duotone.Editor
