using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Items/Weapon", order = Utility.CREATE_ASSET_ORDER_ID)]
public class WeaponItem : EquipableItem {

    public override string ItemType { get { return "Weapon"; } }
    public override EquipmentTypes EquipmentType { get { return EquipmentTypes.Weapon; } }
}
