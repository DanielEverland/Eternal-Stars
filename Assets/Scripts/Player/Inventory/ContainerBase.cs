using System;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBase {

	public ContainerBase(int size)
    {
        Items = new Dictionary<Vector2, ItemStack>();

        _containerSize = size;
        _rows = Mathf.CeilToInt(size / Columns);
    }
    private ContainerBase() { }

    public const int INVENTORY_COLUMNS = 6;

    public event Action<ItemBase> OnItemAdded;
    public event Action<ItemBase> OnItemRemoved;
    public event Action<ItemStack> OnStackUpdated;
    public event Action OnContainerChanged;

    public Dictionary<Vector2, ItemStack> Items { get; private set; }
    public int Columns { get { return INVENTORY_COLUMNS; } }
    public int Rows { get { return _rows; } }
    public int Size { get { return _containerSize; } }

    private readonly int _containerSize;
    private readonly int _rows;
        
    public void Add(Vector2 index, ItemStack stack)
    {
        for (int y = (int)index.y; y >= 0; y--)
        {
            for (int x = (int)index.x; x >= 0; x--)
            {
                Vector2 currentPosition = new Vector2(x, y);
                
                if (Items.ContainsKey(currentPosition))
                {
                    ItemStack existingStack = Items[currentPosition];

                    Vector2 delta = new Vector2()
                    {
                        x = index.x - x,
                        y = index.y - y,
                    };
                    
                    if(delta.x < existingStack.Item.InventorySize.x && delta.y < existingStack.Item.InventorySize.y)
                    {
                        if(existingStack.Item == stack.Item)
                        {
                            existingStack.AddAmount(stack.ItemAmount);
                        }
                        else
                        {
                            throw new Exception("Space is occupied. Call Fits before you add");
                        }

                        return;
                    }
                }
            }
        }

        Items.Add(index, stack);
    }
    public void Remove(ItemStack stack)
    {
        Vector2? index = null;

        foreach (KeyValuePair<Vector2, ItemStack> pair in Items)
        {
            if(pair.Value == stack)
            {
                index = pair.Key;
                break;
            }
        }

        if(index.HasValue)
        {
            Items.Remove(index.Value);

            ItemRemoved(stack.Item);
        }
        else
        {
            throw new ArgumentException();
        }
    }
    /// <summary>
    /// Given a position, returns whether or not said position is occupied by a stack.
    /// </summary>
    public bool Exists(Vector2 position)
    {
        return FindItem(position);
    }
    public void Add(ItemBase item)
    {
        ItemStack stack = new ItemStack(item, this);

        for (int y = 0; y < Rows; y++)
        {
            for (int x = 0; x < Columns; x++)
            {
                Vector2 pos = new Vector2(x, y);

                if (!Items.ContainsKey(pos))
                {
                    Items.Add(pos, stack);

                    ItemAdded(item);
                    return;
                }
                else
                {
                    ContainerSearchQuery query = FindItem(pos);
                    
                    if (query.successful)
                    {
                        if (query.itemStack.Item == item)
                        {
                            Items[pos].AddAmount();

                            StackUpdated(Items[pos]);
                            return;
                        }
                    }
                }
            }
        }
    }
    public bool Fits(ItemBase item, ContainerSearchType searchType = ContainerSearchType.AllowSameType)
    {
        for (int i = 0; i < Size; i += item.InventorySize.x)
        {
            Vector2 position = new Vector2()
            {
                x = i % INVENTORY_COLUMNS * item.InventorySize.x,
                y = (Mathf.CeilToInt((i + 1f) / (float)INVENTORY_COLUMNS) - 1) * item.InventorySize.y,
            };

            if (OutOfBounds(position))
                return false;
            
            if(Fits(item, position, searchType))
            {
                return true;
            }
        }

        return false;
    }
    public bool Fits(ItemBase item, Vector2 index, ContainerSearchType searchType = ContainerSearchType.AllowSameType)
    {
        for (int y = (int)index.y; y > 0; y--)
        {
            for (int x = (int)index.x; x > 0; x--)
            {
                Vector2 currentPosition = new Vector2(x, y);

                if (Items.ContainsKey(currentPosition))
                {
                    ItemStack existingStack = Items[currentPosition];

                    Vector2 delta = new Vector2()
                    {
                        x = index.x - x,
                        y = index.y - y,
                    };
                    
                    if (delta.x < existingStack.Item.InventorySize.x && delta.y < existingStack.Item.InventorySize.y)
                    {
                        if ((existingStack.Item != item) || (existingStack.Item == item && searchType == ContainerSearchType.DisallowSameType))
                        {
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }
    public bool OutOfBounds(Vector2 position)
    {
        return position.x > Columns || position.y > Rows;
    }
    public ContainerSearchQuery FindItem(Vector2 position)
    {
        ContainerSearchQuery query = new ContainerSearchQuery();
        
        //Loop backwards through the inventory. Anything to the left and up will be checked, starting at the given position
        for (int y = (int)position.y; y >= 0; y--)
        {
            for (int x = (int)position.x; x >= 0; x--)
            {
                Vector2 positionToCheck = new Vector2(x, y);
                
                if (Items.ContainsKey(positionToCheck))
                {
                    //Check if the items' bounds contains the paramter position
                    ItemStack currentStack = Items[positionToCheck];

                    //The stacks size must be larger than the delta of the given positions to overlap
                    Vector2 delta = position - positionToCheck;
                    
                    if (delta.x < currentStack.Item.InventorySize.x && delta.y < currentStack.Item.InventorySize.y)
                    {
                        query.availablePosition = positionToCheck;
                        query.itemStack = currentStack;
                        query.successful = true;

                        return query;
                    }
                }
            }
        }

        return query;
    }
    private void ContainerChanged()
    {
        if (OnContainerChanged != null)
            OnContainerChanged.Invoke();
    }
    private void StackUpdated(ItemStack stack)
    {
        if (OnStackUpdated != null)
            OnStackUpdated.Invoke(stack);

        ContainerChanged();
    }
    private void ItemAdded(ItemBase item)
    {
        if (OnItemAdded != null)
            OnItemAdded.Invoke(item);

        ContainerChanged();
    }
    private void ItemRemoved(ItemBase item)
    {
        if (OnItemRemoved != null)
            OnItemRemoved.Invoke(item);

        ContainerChanged();
    }
}
public struct ContainerSearchQuery
{
    public bool successful;
    public Vector2 availablePosition;
    public ItemStack itemStack;

    public static implicit operator bool(ContainerSearchQuery query)
    {
        return query.successful;
    }
    public override bool Equals(object obj)
    {
        return obj.GetHashCode() == this.GetHashCode();
    }
    public override int GetHashCode()
    {
        unchecked
        {
            int i = 13;

            i *= 17 + successful.GetHashCode();
            i *= 17 + availablePosition.GetHashCode();
            i *= 17 + itemStack.GetHashCode();

            return i;
        }
    }
}
public enum ContainerSearchType
{
    AllowSameType,
    DisallowSameType,
}