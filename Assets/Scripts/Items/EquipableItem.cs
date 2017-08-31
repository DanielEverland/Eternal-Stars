using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : ItemBase {

    public EquipmentTypes EquipmentType { get { return _equipmentType; } }
    
    [SerializeField]
    private EquipmentTypes _equipmentType;

    public override string ItemType { get { return EquipmentType.ToString(); } }
}
