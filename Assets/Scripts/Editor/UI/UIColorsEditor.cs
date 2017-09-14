using UnityEditorInternal;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(UIColors))]
public class UIColorsEditor : Editor {

    private ReorderableList list;

    private const float COLOR_FIELD_WIDTH = 80;
    private const float SPACING = 3;
    private const float TOP_PADDING = 2;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("_colorEntries"), true, true, true, true);
        list.drawElementCallback += DrawElement;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
    private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        rect.y += TOP_PADDING;

        EditorGUI.PropertyField(new Rect(rect.x, rect.y, COLOR_FIELD_WIDTH, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Color"), GUIContent.none);
        EditorGUI.PropertyField(new Rect(rect.x + COLOR_FIELD_WIDTH + SPACING, rect.y, rect.width - COLOR_FIELD_WIDTH - SPACING, EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("Key"), GUIContent.none);
    }
}
