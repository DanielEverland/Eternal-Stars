using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomPropertyDrawer(typeof(IntVector2))]
public class IntVector2PropertyDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        Rect fullPropertyRect = new Rect(position.x, position.y, position.width / 1.5f, position.height);

        Rect xRect = new Rect(fullPropertyRect.position.x, fullPropertyRect.position.y,
            fullPropertyRect.width / 2, fullPropertyRect.height);

        Rect yRect = new Rect(xRect.position.x + xRect.width, xRect.position.y,
            xRect.width, xRect.height);

        EditorGUIUtility.labelWidth = 12;
        EditorGUI.PropertyField(xRect, property.FindPropertyRelative("_x"));
        EditorGUI.PropertyField(yRect, property.FindPropertyRelative("_y"));
    }
}
