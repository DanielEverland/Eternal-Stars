using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : ItemBase {
    
    public abstract EquipmentTypes EquipmentType { get; }

    public override void OnRightClick(ItemStack stack)
    {
        base.OnRightClick(stack);

        Player.Instance.EquipmentContainer.TryAdd(stack);
    }
    public override void OnCreatedInInspector()
    {
        base.OnCreatedInInspector();

        _maxStackSize = 1;
    }
}
