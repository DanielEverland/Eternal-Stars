using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject {

    public virtual Rarity Rarity { get { return _rarity; } }
    public virtual string Name { get { return _name; } }
    public virtual IntVector2 InventorySize { get { return _inventorySize; } }
    public virtual Sprite Icon { get { return _icon; } }

    [Header("Base Properties")]

    [SerializeField]
    private Rarity _rarity;
    [SerializeField]
    private string _name;
    [SerializeField]
    private IntVector2 _inventorySize;
    [SerializeField]
    private Sprite _icon;

    public string GetTooltip()
    {
        return string.Format("<color={0}>{1}</color>\n", Rarity.Color.ToHex(), Name);
    }

    #region Object handling shit
    public override bool Equals(object other)
    {
        if (other == null)
            return false;

        if (other is ItemBase)
        {
            return other.GetHashCode() == GetHashCode();
        }

        return false;
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int i = 13;

            i *= 17 + Name.GetHashCode();
            i *= 17 + Rarity.GetHashCode();
            i *= 17 + InventorySize.GetHashCode();

            return i;
        }
    }
    public override string ToString()
    {
        return string.Format("{0} - {1}\nSize: {2}", Name, Rarity, InventorySize);
    }
    public bool Equals(ItemBase other)
    {
        if (other == null)
            return false;

        return other.GetHashCode() == GetHashCode();
    }
    public static bool operator ==(ItemBase a, ItemBase b)
    {
        if(((object)a == null && (object)b != null) || ((object)a != null && (object)b == null))
        {
            return false;
        }
        if((object)a == null && (object)b == null)
        {
            return true;
        }

        return a.GetHashCode() == b.GetHashCode();
    }
    public static bool operator !=(ItemBase a, ItemBase b)
    {
        return !(a == b);
    }
#endregion
}