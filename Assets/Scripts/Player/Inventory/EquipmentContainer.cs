using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContainer : IContainerBase {

    public event Action OnUpdate;
    
    private Dictionary<EquipmentSlotIdentifier, ItemStack> equippedItems = new Dictionary<EquipmentSlotIdentifier, ItemStack>();
    
    public EquipableItem GetItem(EquipmentTypes equipmentType, byte slotIndex)
    {
        return GetItem(new EquipmentSlotIdentifier() { EquipmentType = equipmentType, Index = slotIndex });
    }
    public EquipableItem GetItem(EquipmentSlotIdentifier slotIdentifier)
    {
        if (equippedItems.ContainsKey(slotIdentifier))
        {
            return equippedItems[slotIdentifier].Item as EquipableItem;
        }

        return null;
    }
    public ItemStack GetStack(EquipmentSlotIdentifier slotIdentifier)
    {
        if (equippedItems.ContainsKey(slotIdentifier))
        {
            return equippedItems[slotIdentifier];
        }

        return null;
    }
    public void Add(object index, ItemStack stack)
    {
        if(index is EquipmentSlotIdentifier && stack.Item is EquipableItem)
        {
            Add((EquipmentSlotIdentifier)index, stack);
        }
    }
    public void Add(EquipmentSlotIdentifier slotIdentifier, ItemStack stack)
    {
        equippedItems.Add(slotIdentifier, stack);

        if (OnUpdate != null)
            OnUpdate.Invoke();
    }
    public void Remove(ItemStack stack)
    {
        EquipmentSlotIdentifier? keyToRemove = null;

        foreach (KeyValuePair<EquipmentSlotIdentifier, ItemStack> pair in equippedItems)
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
    public void Remove(EquipmentSlotIdentifier slotIdentifier)
    {
        equippedItems.Remove(slotIdentifier);

        if (OnUpdate != null)
            OnUpdate.Invoke();
    }
    public bool Contains(EquipmentSlotIdentifier slotIdentifier)
    {
        return equippedItems.ContainsKey(slotIdentifier);
    }
    public bool Fits(object index, ItemBase item)
    {
        if(index is EquipmentSlotIdentifier && item is EquipableItem)
        {
            EquipmentSlotIdentifier slotType = (EquipmentSlotIdentifier)index;

            return !equippedItems.ContainsKey(slotType);
        }

        return false;
    }
}
public struct EquipmentSlotIdentifier
{
    public EquipmentTypes EquipmentType;
    public byte Index;

    public static EquipmentSlotIdentifier GetIdentifier(EquipmentTypes equipmentType, byte index)
    {
        return new EquipmentSlotIdentifier() { EquipmentType = equipmentType, Index = index };
    }
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        return obj.GetHashCode() == this.GetHashCode();
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int i = 13;

            i *= 17 + EquipmentType.GetHashCode();
            i *= 17 + Index.GetHashCode();

            return i;
        }
    }
    public override string ToString()
    {
        return string.Format("Type: {0}, Index: {1}", EquipmentType, Index);
    }
}