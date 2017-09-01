using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Implant.asset", menuName = "Items/Implant", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ImplantItem : EquipableItem {

    public override string ItemType { get { return "Implant"; } }
    public override EquipmentTypes EquipmentType { get { return EquipmentTypes.Implant; } }
}
