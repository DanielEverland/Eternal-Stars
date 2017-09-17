using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditor(typeof(ImplantItem))]
public class ImplantEditor : Editor {

    private ImplantItem Target { get { return (ImplantItem)target; } }

    public override void OnInspectorGUI()
    {
        Rect rect = EditorGUILayout.GetControlRect();

        EG_EditorUtility.DrawItemBaseUI(rect, Target, serializedObject);
    }
}
