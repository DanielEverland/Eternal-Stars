using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ConsumableItem.asset", menuName = "Items/Consumable", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ConsumableItem : ItemBase {

    [HideInInspector]
    public List<ItemAction> OnConsumeActions;

    public void Consume()
    {
        for (int i = 0; i < OnConsumeActions.Count; i++)
        {
            OnConsumeActions[i].Action();
        }        

        throw new NotImplementedException("Add a destroy item feature here");
    }
#if UNITY_EDITOR
    public void CreateItemAction(Type type)
    {
        ItemAction newAction = CreateInstance(type) as ItemAction;
        newAction.hideFlags = HideFlags.HideInHierarchy;
        AssetDatabase.AddObjectToAsset(newAction, this);
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAction));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        OnConsumeActions.Add(newAction);
    }
#endif
}
