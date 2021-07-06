using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LocalizationText))]
public class LocalizationTextEditor : UnityEditor.UI.TextEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LocalizationText component = (LocalizationText)target;
        component.Key = EditorGUILayout.TextField("Key", component.Key);
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
