using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ButtonChange))]
public class ButtonChangeEditor : Editor
{
    private SerializedObject _target;
    public SerializedProperty style;
    public SerializedProperty backGroundImg;
    public SerializedProperty scaleTarget;
    public SerializedProperty targetSize;
    public SerializedProperty changedBkTarget;
    public SerializedProperty normalImg;
    public SerializedProperty highlightedImg;
    public SerializedProperty normalColor;
    public SerializedProperty highlightColor;
    private void OnEnable()
    {
        _target = new SerializedObject(target);
        style = _target.FindProperty("style");
        backGroundImg = _target.FindProperty("backGroundImg");
        changedBkTarget = _target.FindProperty("changedBkTarget");
        normalImg = _target.FindProperty("normalImg");
        highlightedImg = _target.FindProperty("highlightedImg");
        scaleTarget = _target.FindProperty("scaleTarget");
        targetSize = _target.FindProperty("targetSize");
        normalColor = _target.FindProperty("normalColor");
        highlightColor = _target.FindProperty("highlightColor");
    }
    public override void OnInspectorGUI()
    {
        _target.Update();
        EditorGUILayout.PropertyField(style);
        //EditorGUILayout.PropertyField(backGroundImg);
        if (style.enumValueIndex == (int)BtnStyle.HideBk)
        {
            EditorGUILayout.PropertyField(backGroundImg);
        }
        else if (style.enumValueIndex == (int)BtnStyle.Scale)
        {
            EditorGUILayout.PropertyField(scaleTarget);
            EditorGUILayout.PropertyField(targetSize);
        }
        else if (style.enumValueIndex == (int)BtnStyle.ChangeBk)
        {
            EditorGUILayout.PropertyField(changedBkTarget);
            EditorGUILayout.PropertyField(normalImg);
            EditorGUILayout.PropertyField(highlightedImg);
        }
        else if (style.enumValueIndex==(int)BtnStyle.ChangeColor) 
        {
            EditorGUILayout.PropertyField(changedBkTarget);
            EditorGUILayout.PropertyField(normalColor);
            EditorGUILayout.PropertyField(highlightColor);
        }
        _target.ApplyModifiedProperties();
    }
}
