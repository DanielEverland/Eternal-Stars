using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConsumableItem : ItemBase, ScriptableObjectManager {

    [HideInInspector]
    public List<ItemAction> OnConsumeActions;

    public override string ItemType { get { return "Consumable"; } }
    
    public override void OnRightClick(ItemStack stack)
    {
        for (int i = 0; i < OnConsumeActions.Count; i++)
        {
            OnConsumeActions[i].DoAction(stack);
        }

        stack.RemoveAmount(1);
    }
    public override string GetTooltipContent()
    {
        string content = base.GetTooltipContent();

        for (int i = 0; i < OnConsumeActions.Count; i++)
        {
            content += "\nUse: " + OnConsumeActions[i].Description;
        }

        return content;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Items/Implant", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRaname<ConsumableItem>();
    }
    public void CreateObject(Type type)
    {
        OnConsumeActions.Add(CreateObject<ItemAction>(type));
    }
    public void ChangeObjectType(ScriptableObject source, Type target)
    {
        int index = OnConsumeActions.IndexOf((ItemAction)source);

        DestroyImmediate(source, true);
        
        OnConsumeActions[index] = CreateObject<ItemAction>(target);
    }
    private T CreateObject<T>(Type type) where T : ScriptableObject
    {
        T newAction = CreateInstance(type) as T;
        newAction.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newAction, this);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAction));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return newAction;
    }
    public void RemoveObject(ScriptableObject source)
    {
        int index = OnConsumeActions.IndexOf((ItemAction)source);

        DestroyImmediate(source, true);

        OnConsumeActions.RemoveAt(index);
    }
#endif
}
