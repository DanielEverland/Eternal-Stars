using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : ItemBase {

    [SerializeField]
    private bool _uniqueEquipped = true;

    public bool UniqueEquipped { get { return _uniqueEquipped; } }
    public abstract EquipmentTypes EquipmentType { get; }

    private bool IsEquipped = false;
    
    protected virtual void OnEquipped(ItemStack stack) { }
    protected virtual void OnUnequipped(ItemStack stack) { }

    public void CallEquipped(ItemStack stack)
    {
        CurrentStack = stack;

        OnEquipped(stack);
    }
    public void CallUnequipped(ItemStack stack)
    {
        CurrentStack = stack;

        OnUnequipped(stack);
    }

    public override string ItemType { get { return EquipmentType.ToString(); } }
    
    public override string GetTooltipFooter()
    {
        return base.GetTooltipFooter() + ((UniqueEquipped == false) ? "\nNon-Unique" : "");
    }
    protected override void OnRightClick(ItemStack stack)
    {
        if (!Player.Instance.EquipmentContainer.Stacks.Contains(stack))
            Player.Instance.EquipmentContainer.TryAdd(stack);
    }
    public override void OnCreatedInInspector()
    {
        base.OnCreatedInInspector();

        _maxStackSize = 1;
    }
}
