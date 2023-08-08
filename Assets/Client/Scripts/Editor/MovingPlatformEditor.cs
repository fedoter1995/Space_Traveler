using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(MovingPlatform))]
public class MovingPlatformEditor : Editor
{
    MovingPlatform currentPlatform;

    private void OnEnable()
    {
        currentPlatform = (MovingPlatform)target;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();


        EditorGUI.BeginChangeCheck();

        if (GUILayout.Button("Save Position"))
        {
            currentPlatform.OnSaveButtonClick();
        }
        if (GUILayout.Button("Delete Last Position"))
        {
            currentPlatform.OnDeleteButtonClick();
        }

        var somethingChanged = EditorGUI.EndChangeCheck();

        if (somethingChanged)
        {
            EditorUtility.SetDirty(currentPlatform);
        }

        serializedObject.ApplyModifiedProperties();


    }
}
