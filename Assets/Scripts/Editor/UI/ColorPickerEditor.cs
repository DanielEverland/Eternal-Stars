using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(ColorPicker))]
[CanEditMultipleObjects()]
public class ColorPickerEditor : Editor {

	private ColorPicker Target { get { return (ColorPicker)target; } }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("Target"));
                
        string key = serializedObject.FindProperty("Key").stringValue;
        int index = (UIColors.Keys.Contains(key)) ? UIColors.Keys.IndexOf(key) : 0;
        
        index = EditorGUILayout.Popup("Key", index, UIColors.Keys.ToArray());
        
        serializedObject.FindProperty("Key").stringValue = UIColors.Keys[index];

        serializedObject.ApplyModifiedProperties();
    }
}
