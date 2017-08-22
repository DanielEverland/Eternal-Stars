using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectManagerEditor {

    private static GUIContent iconToolbarPlus = EditorGUIUtility.IconContent("Toolbar Plus", "|Add to list");

    private static readonly GUIStyle headerBackground = "RL Header";
    private static readonly GUIStyle footerBackground = "RL Footer";
    private static readonly GUIStyle footerButton = "RL FooterButton";
    private static readonly GUIStyle elementBackground = new GUIStyle("RL Element");

    private static GUIStyle elementLabelStyle
    {
        get
        {
            if(true)
            {
                _elementLabelStyle = new GUIStyle(EditorStyles.label);

                _elementLabelStyle.active.textColor = Color.white;
            }

            return _elementLabelStyle;
        }
    }
    private static GUIStyle _elementLabelStyle;

    private const float HEADER_HEIGHT = 18;
    private const float ELEMENT_PADDING = 5;
    private const float DELETE_BUTTON_WIDTH = 50;
    private const float DELETE_BUTTON_HEIGHT = 14;

    private const float FOOTER_WIDTH = 40;
    private const float FOOTER_HEIGHT = 50;

    public static List<T> DrawScriptableObjectList<T>(string label, List<T> list, List<Type> availableTypes, ScriptableObjectManager objectOwner) where T : ScriptableObject
    {
        if (availableTypes.Count <= 0)
        {
            Debug.LogWarning("No available types");
            return list;
        }

        Rect headerRect = GUILayoutUtility.GetRect(0, HEADER_HEIGHT, new GUILayoutOption[] { GUILayout.ExpandWidth(true), });
        
        DrawHeader(headerRect, label);
        DrawElements<T>(list, availableTypes, objectOwner);

        Rect footerRect = GUILayoutUtility.GetRect(0, FOOTER_HEIGHT, new GUILayoutOption[] { GUILayout.ExpandWidth(true), });
        footerRect.y += 2;
        footerRect.x = footerRect.width - 26;
        footerRect.width = FOOTER_WIDTH;

        DrawFooter(footerRect, list, availableTypes, objectOwner);

        return list;
    }
    private static void DrawFooter<T>(Rect rect, List<T> list, List<Type> availableTypes, ScriptableObjectManager objectOwner) where T : ScriptableObject
    {
        if(Event.current.type == EventType.Repaint)
        {
            footerBackground.Draw(rect, false, false, false, false);
        }

        rect.y -= 5;

        if(GUI.Button(rect, iconToolbarPlus, footerButton))
        {
            objectOwner.CreateObject(availableTypes[0]);
        }
    }
    private static void DrawElements<T>(List<T> list, List<Type> availableTypes, ScriptableObjectManager objectOwner) where T : ScriptableObject
    {
        for (int i = 0; i < list.Count; i++)
        {
            T element = list[i];

            float elementHeight = GetElementHeight(element);
            Rect elementHeaderRect = GUILayoutUtility.GetRect(0, EditorGUIUtility.singleLineHeight + ELEMENT_PADDING, new GUILayoutOption[] { GUILayout.ExpandWidth(true), });

            if (Event.current.type == EventType.Repaint)
            {
                int propertyCount = EG_EditorUtility.GetSerializableFields(element.GetType()).Count;

                elementBackground.Draw(new Rect(elementHeaderRect.x, elementHeaderRect.y, elementHeaderRect.width, (propertyCount + 1) * (EditorGUIUtility.singleLineHeight + ELEMENT_PADDING)), false, false, true, false);
            }

            DrawElementHeader(element, elementHeaderRect, availableTypes, objectOwner);
            DrawElementProperties(element, availableTypes, objectOwner);
        }
    }
    private static void DrawElementProperties<T>(T obj, List<Type> availableTypes, ScriptableObjectManager objectOwner) where T : ScriptableObject
    {
        SerializedObject serializedObject = new SerializedObject(obj);
        serializedObject.Update();

        foreach (FieldInfo field in EG_EditorUtility.GetSerializableFields(obj.GetType()))
        {
            SerializedProperty property = serializedObject.FindProperty(field.Name);

            EditorGUILayout.PropertyField(property);
        }

        serializedObject.ApplyModifiedProperties();
    }
    private static void DrawElementHeader<T>(T obj, Rect rect, List<Type> availableTypes, ScriptableObjectManager objectOwner) where T : ScriptableObject
    {
        Rect buttonRect = new Rect(rect.width - DELETE_BUTTON_WIDTH + 10, rect.y + 1, DELETE_BUTTON_WIDTH, EditorGUIUtility.singleLineHeight);
        Rect popupRect = new Rect(rect.x + 5, rect.y + 2, rect.width - DELETE_BUTTON_WIDTH - 10, rect.height);
        
        //Popup
        int elementIndex = availableTypes.IndexOf(obj.GetType());

        int newIndex = EditorGUI.Popup(popupRect, elementIndex, availableTypes.Select(x => x.Name).ToArray());

        if (newIndex != elementIndex)
        {
            objectOwner.ChangeObjectType(obj, availableTypes[newIndex]);
        }

        //Delete button
        if(GUI.Button(buttonRect, "Delete"))
        {
            objectOwner.RemoveObject(obj);
        }
    }
    private static float GetElementHeight<T>(T obj)
    {
        float height = 0;

        //Type selection
        height += EditorGUIUtility.singleLineHeight;
        height += ELEMENT_PADDING;

        return height;
    }
    private static void DrawHeader(Rect rect, string label)
    {
        if (Event.current.type == EventType.Repaint)
        {
            headerBackground.Draw(rect, false, false, false, false);

            rect.x += 4;
            EditorGUI.LabelField(rect, label);
        }
    }
}
