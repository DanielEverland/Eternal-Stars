using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipableItem : ItemBase {

    [SerializeField]
    private bool _uniqueEquipped = true;
    [SerializeField]
    private string _description;

    public bool UniqueEquipped { get { return _uniqueEquipped; } }
    public abstract EquipmentTypes EquipmentType { get; }

    protected string Description { get { return _description; } }
    
    public override string GetTooltipFooter()
    {
        return base.GetTooltipFooter() + ((UniqueEquipped == false) ? "\nNon-Unique" : "");
    }
    public override void OnRightClick(ItemStack stack)
    {
        base.OnRightClick(stack);

        if (!Player.Instance.EquipmentContainer.Stacks.Contains(stack))
            Player.Instance.EquipmentContainer.TryAdd(stack);
    }
    public override void OnCreatedInInspector()
    {
        base.OnCreatedInInspector();

        _maxStackSize = 1;
    }
}
