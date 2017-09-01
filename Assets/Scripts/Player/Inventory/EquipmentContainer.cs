using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContainer : IContainerBase {

    public event Action OnUpdate;
    
    private Dictionary<EquipmentSlotTypes, ItemStack> equippedItems = new Dictionary<EquipmentSlotTypes, ItemStack>();
    
    public EquipableItem GetItem(EquipmentSlotTypes slotType)
    {
        if (equippedItems.ContainsKey(slotType))
        {
            return equippedItems[slotType].Item as EquipableItem;
        }

        return null;
    }
    public ItemStack GetStack(EquipmentSlotTypes slotType)
    {
        if (equippedItems.ContainsKey(slotType))
        {
            return equippedItems[slotType];
        }

        return null;
    }
    public void Add(object index, ItemStack stack)
    {
        if(index is EquipmentSlotTypes && stack.Item is EquipableItem)
        {
            Add((EquipmentSlotTypes)index, stack);
        }
    }
    public void Add(EquipmentSlotTypes slotType, ItemStack stack)
    {
        equippedItems.Add(slotType, stack);

        if (OnUpdate != null)
            OnUpdate.Invoke();
    }
    public void Remove(ItemStack stack)
    {
        EquipmentSlotTypes? keyToRemove = null;

        foreach (KeyValuePair<EquipmentSlotTypes, ItemStack> pair in equippedItems)
        {
            if(pair.Value.Item == stack.Item)
            {
                keyToRemove = pair.Key;
                break;
            }
        }

        if (keyToRemove.HasValue)
        {
            Remove(keyToRemove.Value);
        }
    }
    public void Remove(EquipmentSlotTypes slotType)
    {
        equippedItems.Remove(slotType);

        if (OnUpdate != null)
            OnUpdate.Invoke();
    }
    public bool Contains(EquipmentSlotTypes slotType)
    {
        return equippedItems.ContainsKey(slotType);
    }
    public bool Fits(object index, ItemBase item)
    {
        if(index is EquipmentSlotTypes && item is EquipableItem)
        {
            EquipmentSlotTypes slotType = (EquipmentSlotTypes)index;
            EquipableItem equipment = (EquipableItem)item;

            foreach (CharacterSheet.SubMenu subMenu in CharacterSheet.SubMenus)
            {
                if(subMenu.CompatibleEquipmentType == equipment.EquipmentType)
                {
                    if (subMenu.Contains(slotType))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
