using UnityEditorInternal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConsumableItem : ItemBase, ScriptableObjectManager<ItemAction> {
    
    [HideInInspector]
    public List<ItemAction> OnConsumeActions;

    public override string ItemType { get { return "Consumable"; } }
    
    public override void OnRightClick(ItemStack stack)
    {
        for (int i = 0; i < OnConsumeActions.Count; i++)
        {
            OnConsumeActions[i].DoAction();
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
    string ScriptableObjectManager<ItemAction>.ListHeader { get { return "On Consumed Action"; } }
    List<Type> ScriptableObjectManager<ItemAction>.AvailableTypes { get { return ItemActionManager.AvailableActions; } }
    ReorderableList ScriptableObjectManager<ItemAction>.ReorderableList
    {
        get
        {
            if(reorderableList == null)
            {
                reorderableList = new ReorderableList(OnConsumeActions, typeof(ItemAction));
            }

            return reorderableList;
        }
    }
    private ReorderableList reorderableList;

    [MenuItem("Assets/Create/Items/Implant", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRename<ConsumableItem>();
    }
    void ScriptableObjectManager<ItemAction>.CreateObject(Type type)
    {
        OnConsumeActions.Add(Utility.CreateObject<ItemAction>(type, this));
    }
    void ScriptableObjectManager<ItemAction>.RemoveObject(ScriptableObject source)
    {
        int index = OnConsumeActions.IndexOf((ItemAction)source);

        OnConsumeActions.RemoveAt(index);

        DestroyImmediate(source, true);
    }
#endif
}
