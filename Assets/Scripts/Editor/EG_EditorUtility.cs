using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public static class EG_EditorUtility {

	public static List<T> DrawScriptableObjectList<T>(List<T> list, List<Type> availableTypes, ConsumableItem objectOwner) where T : ScriptableObject
    {
        if(availableTypes.Count <= 0)
        {
            Debug.LogWarning("No available types");
            return list;
        }

        //Create button
        if(GUILayout.Button("Add Object"))
        {
            objectOwner.CreateItemAction(availableTypes[0]);
        }

        //Draw objects
        for (int i = 0; i < list.Count; i++)
        {
            T action = list[i];

            //Draw type selection
            int indexOfAction = availableTypes.IndexOf(action.GetType());

            int selectedActionType = EditorGUILayout.Popup("Type", indexOfAction, availableTypes.Select(x => x.Name).ToArray());

            if(selectedActionType != indexOfAction)
            {
                objectOwner.ChangeObjectType(action, availableTypes[selectedActionType]);
                continue;
            }

            //Draw properties
            SerializedObject obj = new SerializedObject(action);
            obj.Update();

            foreach (FieldInfo field in GetSerializableFields(action.GetType()))
            {
                SerializedProperty property = obj.FindProperty(field.Name);

                EditorGUILayout.PropertyField(property);
            }

            obj.ApplyModifiedProperties();
        }
        
        return list;
    }
    private static List<FieldInfo> GetSerializableFields(Type type)
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
