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
        else
        {
            throw new NullReferenceException("Item " + stack.Item + " doesn't exist in container");
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
    public bool Fits(ItemBase item)
    {
        return item is EquipableItem;
    }
}
