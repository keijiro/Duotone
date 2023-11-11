using UnityEditor;

namespace Duotone.Editor {

[CanEditMultipleObjects]
[CustomEditor(typeof(DuotoneController))]
sealed class DuotoneControllerEditor : UnityEditor.Editor
{
    #region Private members

    #pragma warning disable CS0649

    AutoProperty LowColor;
    AutoProperty HighColor;
    AutoProperty SplitLevel;
    AutoProperty BlackColor;
    AutoProperty BlackLevel;
    AutoProperty WhiteColor;
    AutoProperty WhiteLevel;
    AutoProperty EdgeColor;
    AutoProperty DitherType;
    AutoProperty DitherStrength;

    #pragma warning restore CS0649

    #endregion

    #region Editor implementation

    void OnEnable() => AutoProperty.Scan(this);

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

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
        EditorGUILayout.PropertyField(EdgeColor);
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(DitherType);
        EditorGUILayout.PropertyField(DitherStrength);

        serializedObject.ApplyModifiedProperties();
    }

    #endregion
}

} // namespace Duotone.Editor
