using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ConsumableItem.asset", menuName = "Items/Consumable", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ConsumableItem : ItemBase, ScriptableObjectManager {

    [HideInInspector]
    public List<ItemAction> OnConsumeActions;

    public override string ItemType { get { return "Consumable"; } }

    public override CustomTooltipLoadout TooltipLoadout { get { return _customTooltip; } }
    private static ConsumableItemTooltip _customTooltip = new ConsumableItemTooltip();

    public void Consume()
    {
        for (int i = 0; i < OnConsumeActions.Count; i++)
        {
            OnConsumeActions[i].Action();
        }        

        throw new NotImplementedException("Add a destroy item feature here");
    }
#if UNITY_EDITOR
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
