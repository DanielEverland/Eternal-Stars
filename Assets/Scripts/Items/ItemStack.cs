using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemStack {

	public ItemStack(ItemBase item, int amount = 1)
    {
        Item = item;
        ItemAmount = amount;
    }
    
    public ItemBase Item { get; private set; }
    public int ItemAmount { get; private set; }

    public void AddAmount(int value = 1)
    {
        ItemAmount += value;
    }
}
