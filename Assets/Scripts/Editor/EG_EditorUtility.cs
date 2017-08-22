using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;

public static class EG_EditorUtility {

    public static List<T> DrawScriptableObjectList<T>(string label, List<T> list, List<Type> availableTypes, ScriptableObjectManager objectOwner) where T : ScriptableObject
    {
        return ScriptableObjectManagerEditor.DrawScriptableObjectList<T>(label, list, availableTypes, objectOwner);
    }
    public static List<FieldInfo> GetSerializableFields(Type type)
    {
        List<FieldInfo> serializableInfo = new List<FieldInfo>();

        foreach (FieldInfo field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
        {
            if(field.IsPublic && field.FieldType.IsSerializable)
            {
                serializableInfo.Add(field);
            }
            else if(!field.IsPublic && field.GetCustomAttributes(typeof(SerializeField), true).Length > 0)
            {
                serializableInfo.Add(field);
            }
        }
        return serializableInfo;
    }
    public static void DrawHeader(string label)
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
    }
}
