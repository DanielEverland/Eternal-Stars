using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplantItem : EquipableItem, ScriptableObjectManager<ItemTrigger>, ScriptableObjectManager<ItemAction> {

    public override string ItemType { get { return "Implant"; } }
    public override EquipmentTypes EquipmentType { get { return EquipmentTypes.Implant; } }

    [SerializeField, Range(0, 1)]
    private float _procChance;
    [SerializeField]
    private List<ItemTrigger> _procTriggers;
    [SerializeField]
    private List<ItemAction> _procActions;

    public override string Description
    {
        get
        {
            return base.Description.Replace("%", Mathf.RoundToInt(_procChance * 100) + "%");
        }
    }

    public override string GetTooltipContent()
    {
        return base.GetTooltipContent() + "\n" + Description;
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Items/Implant", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRename<ImplantItem>();
    }
    void ScriptableObjectManager<ItemAction>.CreateObject(Type type)
    {
        _procActions.Add(Utility.CreateObject<ItemAction>(type, this));
    }
    void ScriptableObjectManager<ItemTrigger>.CreateObject(Type type)
    {
        _procTriggers.Add(Utility.CreateObject<ItemTrigger>(type, this));
    }
    void ScriptableObjectManager<ItemAction>.RemoveObject(ScriptableObject source)
    {
        int index = _procActions.IndexOf((ItemAction)source);

        DestroyImmediate(source, true);

        _procActions.RemoveAt(index);
    }
    void ScriptableObjectManager<ItemTrigger>.RemoveObject(ScriptableObject source)
    {
        int index = _procTriggers.IndexOf((ItemTrigger)source);

        DestroyImmediate(source, true);

        _procTriggers.RemoveAt(index);
    }
#endif
}
