using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentContainer : IContainerBase {

    public event Action OnUpdate;
    public event Action<EquipableItem> OnItemAdded;
    public event Action<EquipableItem> OnItemRemoved;
    
    public IEnumerable<WeaponBase> Weapons { get { return Stacks.Where(x => x.Item is WeaponBase).Select(x => x.Item as WeaponBase); } }
    public IEnumerable<ImplantItem> Implants { get { return Stacks.Where(x => x.Item is ImplantItem).Select(x => x.Item as ImplantItem); } }

    public IEnumerable<EquipmentSlotIdentifier> SlotKeys { get { return equippedItems.Keys; } }
    public IEnumerable<ItemStack> Stacks { get { return equippedItems.Values.Where(x => x != null); } }
    private Dictionary<EquipmentSlotIdentifier, ItemStack> equippedItems = new Dictionary<EquipmentSlotIdentifier, ItemStack>()
    {
        { new EquipmentSlotIdentifier(EquipmentTypes.Implant, 0), null },
        { new EquipmentSlotIdentifier(EquipmentTypes.Implant, 1), null },
        { new EquipmentSlotIdentifier(EquipmentTypes.Implant, 2), null },

        { new EquipmentSlotIdentifier(EquipmentTypes.Weapon, 0), null },
        { new EquipmentSlotIdentifier(EquipmentTypes.Weapon, 1), null },
        { new EquipmentSlotIdentifier(EquipmentTypes.Weapon, 2), null },
    };
    
    public bool ContainsType(EquipmentTypes type)
    {
        foreach (ItemStack stack in equippedItems.Where(x => x.Key.EquipmentType == type).Select(x => x.Value))
        {
            if (stack != null)
                return true;
        }

        return false;
    }
    public bool ContainsType(EquipableItem item)
    {
        foreach (ItemStack stack in Stacks)
        {
            if (stack.Item.Equals(item))
                return true;
        }

        return false;
    }
    public bool TryAdd(ItemStack stack)
    {
        if (!(stack.Item is EquipableItem))
            return false;
        
        EquipableItem item = stack.Item as EquipableItem;
        EquipmentSlotIdentifier? key = null;

        if (item.UniqueEquipped)
        {
            if(ContainsType(item))
            {
                return false;
            }
        }

        foreach (KeyValuePair<EquipmentSlotIdentifier, ItemStack> pair in equippedItems)
        {
            if(pair.Key.EquipmentType == item.EquipmentType && pair.Value == null)
            {
                key = pair.Key;
                break;
            }
        }

        if(key.HasValue)
        {
            stack.RemoveAmount(1);

            equippedItems[key.Value] = new ItemStack(item, this);

            ItemAdded(item);

            return true;
        }
        else
        {
            return false;
        }
    }
    public EquipableItem GetItem(EquipmentTypes type)
    {
        foreach (ItemStack stack in equippedItems.Where(x => x.Key.EquipmentType == type).Select(x => x.Value))
        {
            if (stack != null)
                return stack.Item as WeaponBase;
        }

        return null;
    }
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
        if (!(stack.Item is EquipableItem))
            throw new ArgumentException("Calls fits before add");

        EquipableItem item = stack.Item as EquipableItem;

        if (item.UniqueEquipped)
        {
            if (ContainsType(item))
            {
                throw new ArgumentException("Calls fits before add");
            }
        }

        equippedItems[slotIdentifier] = stack;

        ItemAdded(stack.Item as EquipableItem);
    }
    public void Remove(ItemStack stack)
    {
        EquipmentSlotIdentifier? keyToRemove = null;

        foreach (KeyValuePair<EquipmentSlotIdentifier, ItemStack> pair in equippedItems)
        {
            if(pair.Value == stack)
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
        ItemRemoved(equippedItems[slotIdentifier].Item as EquipableItem);

        equippedItems[slotIdentifier] = null;
    }
    public bool Contains(EquipmentSlotIdentifier slotIdentifier)
    {
        return equippedItems[slotIdentifier] != null;
    }
    public bool Fits(object index, ItemBase item)
    {
        if (!(item is EquipableItem))
            return false;

        EquipableItem equipableItem = item as EquipableItem;

        if (equipableItem.UniqueEquipped)
        {
            if (ContainsType(equipableItem))
            {
                return false;
            }
        }

        if (index is EquipmentSlotIdentifier && item is EquipableItem)
        {
            EquipmentSlotIdentifier slotType = (EquipmentSlotIdentifier)index;

            return equippedItems[slotType] == null;
        }

        return false;
    }
    public void ItemAdded(EquipableItem item)
    {
        item.OnEquipped();

        if (OnItemAdded != null)
            OnItemAdded.Invoke(item);

        Update();
    }
    public void ItemRemoved(EquipableItem item)
    {
        item.OnUnequipped();

        if (OnItemRemoved != null)
            OnItemRemoved.Invoke(item);

        Update();
    }
    private void Update()
    {
        if (OnUpdate != null)
            OnUpdate.Invoke();
    }
}
public struct EquipmentSlotIdentifier
{
    public EquipmentSlotIdentifier(EquipmentTypes type, byte index)
    {
        this.EquipmentType = type;
        this.Index = index;
    }

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

        if(obj is EquipmentSlotIdentifier)
        {
            EquipmentSlotIdentifier otherIdentifier = (EquipmentSlotIdentifier)obj;

            return otherIdentifier.EquipmentType == this.EquipmentType && otherIdentifier.Index == this.Index;
        }
        else
        {
            return false;
        }
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int i = 13;

            i *= 17 * EquipmentType.GetHashCode();
            i *= 17 * Index.GetHashCode();

            return i;
        }
    }
    public override string ToString()
    {
        return string.Format("Type: {0}, Index: {1}", EquipmentType, Index);
    }
}