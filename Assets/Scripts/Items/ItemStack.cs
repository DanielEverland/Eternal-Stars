using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack {

	public ItemStack(ItemBase item, IContainerBase owner, int amount = 1)
    {
        Item = UnityEngine.Object.Instantiate(item);
        ItemAmount = amount;
        Container = owner;

        if(amount > item.MaxStackSize)
        {
            throw new ArgumentException("Can't have a larger stack than the item will allow");
        }
    }
    
    public ItemBase Item { get; private set; }
    public int ItemAmount { get; private set; }
    public IContainerBase Container { get; private set; }

    public event Action OnUpdate;

    public void ChangeContainer(IContainerBase newContainer)
    {
        Container = newContainer;
    }
    public void AddAmount(int value = 1)
    {
        if (ItemAmount + value > Item.MaxStackSize)
        {
            throw new ArgumentException("Can't have a larger stack than the item will allow");
        }

        ItemAmount += value;

        PushUpdate();
    }
    public void RemoveAmount(int value = 1)
    {
        ItemAmount -= value;

        PushUpdate();

        if (ItemAmount <= 0)
        {
            Container.Remove(this);
        }
    }
    private void PushUpdate()
    {
        if (OnUpdate != null)
            OnUpdate.Invoke();
    }
}
