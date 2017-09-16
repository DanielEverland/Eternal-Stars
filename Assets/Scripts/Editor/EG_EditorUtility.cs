using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public static class EG_EditorUtility {

    private static GUIStyle SpriteButtonStyle
    {
        get
        {
            if(_spriteButtonStyle == null)
            {
                _spriteButtonStyle = new GUIStyle();

                _spriteButtonStyle.normal.background = SpriteFieldBackground;
            }

            return _spriteButtonStyle;
        }
    }
    private static GUIStyle _spriteButtonStyle;

    private static GUIStyle SelectButtonStyle
    {
        get
        {
            if(_selectButtonStyle == null)
            {
                _selectButtonStyle = new GUIStyle();

                _selectButtonStyle.normal.background = SelectButtonBackground;
            }

            return _selectButtonStyle;
        }
    }
    private static GUIStyle _selectButtonStyle;
    
    private static GUIStyle spriteTextureStyle = new GUIStyle();
    private static GUIStyle largeTextFieldStyle = new GUIStyle(EditorStyles.textField)
    {
        fontSize = 13,
    };

    private const float NAME_HEIGHT = 20;
    private const float SPACING = 3;
    private const float SPRITE_TEXTURE_PADDING = 3;
    private const float SPRITE_FIELD_SIZE = 80;
    private const float SELECT_WIDTH = 36;
    private const float SELECT_HEIGHT = 12;

    private static Texture2D SpriteFieldBackground { get { return ObjectImporter.SpriteFieldBackground; } }
    private static Texture2D SelectButtonBackground { get { return ObjectImporter.SelectButtonBackground; } }
    
    public static void DrawEquipmentUI(Rect rect, EquipableItem item, SerializedObject obj)
    {
        EditorGUIUtility.labelWidth = 70;
        obj.FindProperty("_icon").objectReferenceValue = DrawSprite(rect, item.Icon);

        //Name
        Rect nameRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), NAME_HEIGHT);
        obj.FindProperty("_name").stringValue = EditorGUI.TextField(nameRect, item.Name, largeTextFieldStyle);

        rect.y += nameRect.height + SPACING;

        //Description
        string description = obj.FindProperty("_description").stringValue;
        Rect descriptionRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), EditorGUIUtility.singleLineHeight);
        description = EditorGUI.TextField(descriptionRect, new GUIContent("Description", item.Description), description);
        obj.FindProperty("_description").stringValue = description;

        rect.y += descriptionRect.height + SPACING;
        
        //Rarity
        List<Rarity> rarities = Rarity.AllRarities;
        Rarity rarity = obj.FindProperty("_rarity").objectReferenceValue as Rarity;
        int index = rarities.IndexOf(rarity);
        Rect rarityRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), EditorGUIUtility.singleLineHeight);
        index = EditorGUI.Popup(rarityRect, "Rarity", index, rarities.Select(x => x.Name).ToArray());
        obj.FindProperty("_rarity").objectReferenceValue = rarities[index];

        rect.y += rarityRect.height + SPACING;
        
        //Max Stack Size
        byte maxStackSize = (byte)obj.FindProperty("_maxStackSize").intValue;
        Rect maxStackSizeRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), EditorGUIUtility.singleLineHeight);
        maxStackSize = (byte)EditorGUI.IntSlider(maxStackSizeRect, new GUIContent("Max Stack", ""), maxStackSize, 1, 255);
        obj.FindProperty("_maxStackSize").intValue = maxStackSize;

        rect.y += maxStackSizeRect.height + SPACING;

        //Inventory Size
        float widthOfSliderField = (((rect.width - EditorGUIUtility.labelWidth) - SPACING) / 2);
        Rect labelRect = new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight);
        Rect xRect = new Rect(labelRect.x + labelRect.width, rect.y, widthOfSliderField, EditorGUIUtility.singleLineHeight);
        Rect yRect = new Rect(xRect.x + xRect.width + SPACING, rect.y, widthOfSliderField, EditorGUIUtility.singleLineHeight);

        IntVector2 value = item.InventorySize;
        float oldWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 12;

        EditorGUI.LabelField(labelRect, "Size");
        EditorGUI.IntSlider(xRect, "X", value.x, 1, ItemBase.LARGEST_SIZE);
        EditorGUI.IntSlider(yRect, "Y", value.y, 1, ItemBase.LARGEST_SIZE);

        EditorGUIUtility.labelWidth = oldWidth;

        obj.ApplyModifiedProperties();
    }
    public static Sprite DrawSprite(Rect rect, Sprite sprite, float size = SPRITE_FIELD_SIZE)
    {
        Rect buttonRect = new Rect(rect.x, rect.y, size, size);

        int controlID = EditorGUIUtility.GetControlID(FocusType.Passive);
        
        if (GUI.Button(buttonRect, GUIContent.none, SpriteButtonStyle))
        {
            EditorGUIUtility.ShowObjectPicker<Sprite>(sprite, false, "", controlID);
        }
        
        if(Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == controlID)
        {
            sprite = (Sprite)EditorGUIUtility.GetObjectPickerObject();
        }

        if(Event.current.type == EventType.repaint)
        {
            if(sprite != null)
            {
                Rect spriteRect = new Rect(buttonRect.x + SPRITE_TEXTURE_PADDING, buttonRect.y + SPRITE_TEXTURE_PADDING, buttonRect.width - SPRITE_TEXTURE_PADDING * 2, buttonRect.height - SPRITE_TEXTURE_PADDING * 2);

                spriteTextureStyle.normal.background = sprite.texture;
                spriteTextureStyle.Draw(spriteRect, GUIContent.none, false, false, false, false);
            }

            //Draw select rect
            Rect selectRect = new Rect(buttonRect.x + buttonRect.width - SELECT_WIDTH - 1, buttonRect.y + buttonRect.height - SELECT_HEIGHT - 1, SELECT_WIDTH, SELECT_HEIGHT);
            SelectButtonStyle.Draw(selectRect, GUIContent.none, 0);
        }

        if(sprite == null)
        {
            GUIStyle style = GUI.skin.GetStyle("Label");
            style.alignment = TextAnchor.UpperCenter;

            GUI.Label(buttonRect, "None\n(Sprite)");
        }        

        return sprite;
    }
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

    private static class ObjectImporter
    {
        public static Texture2D SpriteFieldBackground
        {
            get
            {
                if(_spriteFieldBackground == null)
                {
                    _spriteFieldBackground = GetAsset<Texture2D>("SpriteFieldBackground");
                }

                return _spriteFieldBackground;
            }
        }
        private static Texture2D _spriteFieldBackground;

        public static Texture2D SelectButtonBackground
        {
            get
            {
                if (_selectButtonBackground == null)
                {
                    _selectButtonBackground = GetAsset<Texture2D>("SelectButton");
                }

                return _selectButtonBackground;
            }
        }
        private static Texture2D _selectButtonBackground;

        private static T GetAsset<T>(string fileName) where T : UnityEngine.Object
        {
            string[] allGUIDs = AssetDatabase.FindAssets(fileName);

            for (int i = 0; i < allGUIDs.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(allGUIDs[i]);

                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

                if(obj is T)
                {
                    return obj as T;
                }
            }

            throw new NullReferenceException("Couldn't find asset " + fileName);
        }
    }
}
