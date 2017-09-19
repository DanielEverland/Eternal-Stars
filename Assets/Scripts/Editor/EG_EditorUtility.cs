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
    private const int RARITY_INDICATOR_HEIGHT = 3;

    private static Texture2D SpriteFieldBackground { get { return ObjectImporter.SpriteFieldBackground; } }
    private static Texture2D SelectButtonBackground { get { return ObjectImporter.SelectButtonBackground; } }
    
    public static void DrawImplantUI(ImplantItem item, SerializedObject obj)
    {
        DrawEquipableItemUI(item, obj);

        Rect rect = EditorGUILayout.GetControlRect();
        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80;

        SerializedProperty property = null;

        //Proc chance
        property = obj.FindProperty("_procChance");
        Rect procChanceRect = new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight);
        property.floatValue = EditorGUI.Slider(procChanceRect, "Proc Chance", property.floatValue, 0, 1);

        EditorGUILayout.Space();

        EditorGUIUtility.labelWidth = oldLabelWidth;
        //Proc triggers
        List<ItemTrigger> triggers = (List<ItemTrigger>)item.GetType().GetField("_procTriggers", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(item);
        triggers = DrawScriptableObjectList<ItemTrigger>(item);

        EditorGUILayout.Space();

        //Proc triggers
        List<ItemAction> actions = (List<ItemAction>)item.GetType().GetField("_procActions", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(item);
        actions = DrawScriptableObjectList<ItemAction>(item);

        EditorGUILayout.Space();

        obj.ApplyModifiedProperties();
        EditorGUIUtility.labelWidth = oldLabelWidth;
    }
    public static void DrawEquipableItemUI(EquipableItem item, SerializedObject obj)
    {
        DrawItemBaseUI(item, obj);

        Rect rect = EditorGUILayout.GetControlRect();
        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 70;

        SerializedProperty property = null;

        //Unique equipped
        float preUniqueEquippedLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 80;
        Rect uniqueEquippedRect = new Rect(rect.x, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight);
        property = obj.FindProperty("_uniqueEquipped");
        property.boolValue = EditorGUI.Toggle(uniqueEquippedRect, "Unique Equip", property.boolValue);
        EditorGUIUtility.labelWidth = preUniqueEquippedLabelWidth;

        rect.y += uniqueEquippedRect.height + SPACING;

        obj.ApplyModifiedProperties();
        EditorGUIUtility.labelWidth = oldLabelWidth;
    }
    public static void DrawItemBaseUI(ItemBase item, SerializedObject obj)
    {
        Rect rect = EditorGUILayout.GetControlRect();

        float oldLabelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 70;
        SerializedProperty property = null;

        //Sprite
        obj.FindProperty("_icon").objectReferenceValue = DrawSprite(rect, item.Icon);

        //Name
        property = obj.FindProperty("_name");
        Rect nameRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), NAME_HEIGHT);
        property.stringValue = EditorGUI.TextField(nameRect, property.stringValue, largeTextFieldStyle);

        rect.y += nameRect.height + SPACING;

        //Description
        property = obj.FindProperty("_description");
        Rect descriptionRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), EditorGUIUtility.singleLineHeight);
        property.stringValue = EditorGUI.TextField(descriptionRect, new GUIContent("Description", item.Description.Trim()), property.stringValue);

        rect.y += descriptionRect.height + SPACING;

        //-----Rarity-----
        List<Rarity> rarities = Rarity.AllRarities;
        Rarity rarity = obj.FindProperty("_rarity").objectReferenceValue as Rarity;
        Rect rarityRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), EditorGUIUtility.singleLineHeight);

        //Rarity Indicator
        Rect indicatorRect = new Rect(rarityRect.x + EditorGUIUtility.labelWidth, rarityRect.y, rarityRect.width - EditorGUIUtility.labelWidth, RARITY_INDICATOR_HEIGHT);

        for (int i = 0; i < RARITY_INDICATOR_HEIGHT; i++)
        {
            EditorGUI.DrawRect(indicatorRect, rarity.Color);

            indicatorRect.y -= 1;
            indicatorRect.x += 1;
            indicatorRect.width -= 2;
        }        

        //Rarity Dropdown
        int index = rarities.IndexOf(rarity);
        index = EditorGUI.Popup(rarityRect, "Rarity", index, rarities.Select(x => x.Name).ToArray());
        obj.FindProperty("_rarity").objectReferenceValue = rarities[index];

        rect.y += rarityRect.height + SPACING;
        
        //Max Stack Size
        property = obj.FindProperty("_maxStackSize");
        Rect maxStackSizeRect = new Rect(rect.x + SPRITE_FIELD_SIZE + SPACING, rect.y, rect.width - (SPRITE_FIELD_SIZE + SPACING), EditorGUIUtility.singleLineHeight);
        property.intValue = EditorGUI.IntSlider(maxStackSizeRect, new GUIContent("Max Stack", ""), property.intValue, 1, 255);

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
        value.x = EditorGUI.IntSlider(xRect, "X", value.x, 1, ItemBase.LARGEST_SIZE);
        value.y = EditorGUI.IntSlider(yRect, "Y", value.y, 1, ItemBase.LARGEST_SIZE);

        EditorGUIUtility.labelWidth = oldWidth;

        item.GetType().GetField("_inventorySize", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, value);
        
        obj.ApplyModifiedProperties();
        GUILayoutUtility.GetRect(rect.width, rect.y);
        EditorGUIUtility.labelWidth = oldLabelWidth;
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
                Texture2D texture = sprite.ToTexture();
                spriteTextureStyle.normal.background = texture;
                
                int fittedWidth;
                int fittedHeight;
                float aspectRatio = (float)texture.height / (float)texture.width;
                float availableWidth = buttonRect.width - SPRITE_TEXTURE_PADDING * 2;
                float availableHeight = buttonRect.height - SPRITE_TEXTURE_PADDING * 2;

                if (texture.width > texture.height)
                {
                    fittedWidth = (int)availableWidth;
                    fittedHeight = (int)((float)availableWidth * aspectRatio);
                }
                else
                {
                    fittedWidth = (int)((float)availableHeight / aspectRatio);
                    fittedHeight = (int)availableHeight;
                }

                float yOffset = availableHeight / 2 - fittedHeight / 2;

                Rect spriteRect = new Rect(buttonRect.x + SPRITE_TEXTURE_PADDING, buttonRect.y + SPRITE_TEXTURE_PADDING + yOffset, fittedWidth, fittedHeight);

                spriteTextureStyle.Draw(spriteRect, GUIContent.none, false, false, false, false);        
            }

            //Draw select rect
            Rect selectRect = new Rect(buttonRect.x + buttonRect.width - SELECT_WIDTH - 1, buttonRect.y + buttonRect.height - SELECT_HEIGHT - 1, SELECT_WIDTH, SELECT_HEIGHT);
            SelectButtonStyle.Draw(selectRect, GUIContent.none, 0);
        }

        GUIStyle labelStyle = GUI.skin.GetStyle("Label");
        labelStyle.alignment = TextAnchor.UpperCenter;

        if (sprite == null)
        {
            GUI.Label(buttonRect, "None\n(Sprite)");
        }

        return sprite;
    }
    public static List<T> DrawScriptableObjectList<T>(ScriptableObjectManager<T> objectOwner) where T : ScriptableObject
    {
        return ScriptableObjectManagerEditor.DrawScriptableObjectList<T>(objectOwner);
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
