using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImplantItem : EquipableItem {

    public override string ItemType { get { return "Implant"; } }
    public override EquipmentTypes EquipmentType { get { return EquipmentTypes.Implant; } }
        
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Items/Implant", priority = Utility.CREATE_ASSET_ORDER_ID)]
    private static void CreateAssetImplant()
    {
        Utility.CreateItemAndRaname<ImplantItem>();
    }
#endif
}
