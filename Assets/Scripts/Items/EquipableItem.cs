using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : ItemBase {
    
    public abstract EquipmentTypes EquipmentType { get; }
}
