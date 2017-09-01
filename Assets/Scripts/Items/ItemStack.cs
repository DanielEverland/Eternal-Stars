using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack {

	public ItemStack(ItemBase item, ContainerBase owner, int amount = 1)
    {
        Item = item;
        ItemAmount = amount;
        Container = owner;
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
