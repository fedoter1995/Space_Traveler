using SpaceTraveler.GameStructures.Stats;
using UnityEditor;

[CustomEditor(typeof(BaseStat))]
[CanEditMultipleObjects]
public class BaseStatEditor : Editor
{
/*    private SerializedProperty _name;
    private SerializedProperty statPresetName;
    void OnEnable()
    {
        _name = serializedObject.FindProperty("_name");
        statPresetName = serializedObject.FindProperty("Name");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
            EditorGUILayout.PropertyField(_name);
            EditorGUILayout.PropertyField(statPresetName);
        serializedObject.ApplyModifiedProperties();

    }*/
}
