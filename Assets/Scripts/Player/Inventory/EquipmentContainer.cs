using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContainer : IContainerBase {
    
    private Dictionary<EquipmentSlotTypes, EquipableItem> equippedItems = new Dictionary<EquipmentSlotTypes, EquipableItem>();
    
    public EquipableItem GetItem(EquipmentSlotTypes slotType)
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
            Add((EquipmentSlotTypes)index, stack.Item as EquipableItem);
        }
    }
    public void Add(EquipmentSlotTypes slotType, EquipableItem item)
    {
        equippedItems.Add(slotType, item);
    }
    public void Remove(ItemStack stack)
    {
        EquipmentSlotTypes? keyToRemove = null;

        foreach (KeyValuePair<EquipmentSlotTypes, EquipableItem> pair in equippedItems)
        {
            if(pair.Value == stack.Item)
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
