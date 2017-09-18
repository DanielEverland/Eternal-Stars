using UnityEditorInternal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectManagerEditor {
        
    private const float ELEMENT_SPACING = 3;

    private static string currentHeaderLabel;
    private static List<Type> currentAvailableTypes;

    private static Action<int> createItemDelegate;
    private static Action<int> removeItemDelegate;
    private static Func<int, ScriptableObject> getObjectDelegate;
    
    public static List<T> DrawScriptableObjectList<T>(ScriptableObjectManager<T> objectOwner) where T : ScriptableObject
    {
        currentAvailableTypes = objectOwner.AvailableTypes;
        ReorderableList reorderableList = objectOwner.ReorderableList;
        List<T> list = (List<T>)reorderableList.list;

        if (currentAvailableTypes.Count <= 0)
        {
            Debug.LogWarning("No available types");
            return list;
        }

        currentHeaderLabel = objectOwner.ListHeader;

        createItemDelegate = x => { objectOwner.CreateObject(currentAvailableTypes[x]); };
        removeItemDelegate = x => { objectOwner.RemoveObject(list[x]); };
        getObjectDelegate = x => { return list[x]; };
        reorderableList.elementHeightCallback = x => { return GetHeight(x); };
        
        reorderableList.drawHeaderCallback = DrawHeader;
        reorderableList.onAddCallback = CreateItem;
        reorderableList.onRemoveCallback = RemoveItem;
        reorderableList.drawElementCallback = DrawElement;
        
        reorderableList.DoLayoutList();

        return list;
    }
    private static void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        DrawHeader(ref rect, index);
        DrawFields(ref rect, index);
    }
    private static void DrawHeader(ref Rect rect, int index)
    {
        ScriptableObject obj = getObjectDelegate(index);
        int selectedObjectIndex = currentAvailableTypes.IndexOf(obj.GetType());
        int oldIndex = selectedObjectIndex;

        selectedObjectIndex = EditorGUI.Popup(
            new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
            selectedObjectIndex,
            currentAvailableTypes.Select(x => x.Name).ToArray());

        if(oldIndex != selectedObjectIndex)
        {
            removeItemDelegate.Invoke(index);
            createItemDelegate.Invoke(selectedObjectIndex);
        }

        rect.y += EditorGUIUtility.singleLineHeight;
    }
    private static void DrawFields(ref Rect rect, int index)
    {
        SerializedObject serializedObject = new SerializedObject(getObjectDelegate(index));
        serializedObject.Update();

        foreach (FieldInfo field in EG_EditorUtility.GetSerializableFields(serializedObject.targetObject.GetType()))
        {
            SerializedProperty property = serializedObject.FindProperty(field.Name);

            EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), property);

            rect.y += EditorGUIUtility.singleLineHeight + ELEMENT_SPACING;
        }

        serializedObject.ApplyModifiedProperties();
    }
    private static float GetHeight(int index)
    {
        SerializedObject serializedObject = new SerializedObject(getObjectDelegate(index));

        return EditorGUIUtility.singleLineHeight + ELEMENT_SPACING //Header
            + EG_EditorUtility.GetSerializableFields(serializedObject.targetObject.GetType()).Count
                * (EditorGUIUtility.singleLineHeight + ELEMENT_SPACING); //Fields
    }
    private static void CreateItem(ReorderableList list)
    {
        createItemDelegate.Invoke(0);
    }
    private static void RemoveItem(ReorderableList list)
    {
        removeItemDelegate.Invoke(list.index);
    }
    private static void DrawHeader(Rect rect)
    {
        EditorGUI.LabelField(rect, currentHeaderLabel);
    }
}
