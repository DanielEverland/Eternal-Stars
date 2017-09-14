using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : ItemBase {
    
    public abstract EquipmentTypes EquipmentType { get; }

    private bool IsEquipped
    {
        get
        {
            foreach (ItemStack stack in Player.Instance.EquipmentContainer.Stacks)
            {
                if(stack.Item == this)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public override void OnRightClick(ItemStack stack)
    {
        base.OnRightClick(stack);

        if(!IsEquipped)
            Player.Instance.EquipmentContainer.TryAdd(stack);
    }
    public override void OnCreatedInInspector()
    {
        base.OnCreatedInInspector();

        _maxStackSize = 1;
    }
}
