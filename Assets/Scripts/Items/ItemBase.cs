using UnityEditor;
using UnityEngine.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject {
    
    public virtual Rarity Rarity { get { return _rarity; } }
    public virtual string Name { get { return _name; } }
    public virtual IntVector2 InventorySize { get { return _inventorySize; } }
    public virtual Sprite Icon { get { return _icon; } }
    public virtual byte MaxStackSize { get { return _maxStackSize; } }
    public virtual string Description { get { return _description; } }

    public virtual CustomTooltipLoadout TooltipLoadout { get { return null; } }

    public abstract string ItemType { get; }

    public const int LARGEST_SIZE = 5;

    [Header("Base Properties")]

    [SerializeField]
    protected Rarity _rarity;
    [SerializeField]
    protected string _name = "";
    [SerializeField]
    protected string _description = "";
    [SerializeField]
    protected IntVector2 _inventorySize = new IntVector2(1, 1);
    [SerializeField]
    protected Sprite _icon;
    [Range(1, 255), SerializeField]
    protected byte _maxStackSize = 255;
    [SerializeField]
    private string _itemID;

    protected ItemStack CurrentStack { get; set; }

    public virtual void OnUpdate(ItemStack stack)
    {
        CurrentStack = stack;
    }
    public virtual void OnRightClick(ItemStack stack)
    {
        CurrentStack = stack;
    }
        
    public virtual string GetTooltipContent()
    {
        return "";
    }
    public virtual string GetTooltipFooter()
    {
        return string.Format(@"Stack Size: {0}", MaxStackSize);
    }
    public virtual void OnCreatedInInspector()
    {
        _rarity = Rarity.AllRarities[0];
        _itemID = GUID.Generate().ToString().ToUpper();
    }
    
    #region Object handling shit
    public override bool Equals(object other)
    {
        if (other == null)
            return false;

        if (other is ItemBase)
        {
            ItemBase otherItem = (ItemBase)other;

            return _itemID == otherItem._itemID;
        }

        return false;
    }
    public override int GetHashCode()
    {
        return _itemID.GetHashCode();
    }
    public override string ToString()
    {
        return string.Format("{0} - {1}\nSize: {2}", Name, Rarity, InventorySize);
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