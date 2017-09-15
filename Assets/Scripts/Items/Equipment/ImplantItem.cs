using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplantItem : EquipableItem {

    public override string ItemType { get { return "Implant"; } }
    public override EquipmentTypes EquipmentType { get { return EquipmentTypes.Implant; } }

    [SerializeField, Range(0, 1)]
    private float ProcChance;
    [SerializeField]
    private List<ItemTrigger> ProcTriggers;
    [SerializeField]
    private List<ItemAction> ProcActions;
        
    public override string GetTooltipContent()
    {
        return base.GetTooltipContent() + "\n" + Description.Replace("%", Mathf.RoundToInt(ProcChance * 100) + "%");
    }

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Items/Implant", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRaname<ImplantItem>();
    }
#endif
}
