using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : ItemBase {

    [SerializeField]
    private bool _uniqueEquipped = true;

    public bool UniqueEquipped { get { return _uniqueEquipped; } }
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

    public override string GetTooltipFooter()
    {
        return base.GetTooltipFooter() + ((UniqueEquipped == false) ? "\nNon-Unique" : "");
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
