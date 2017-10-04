using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack {

	public ItemStack(ItemBase item, IContainerBase owner, int amount = 1)
    {
        Item = item;
        ItemAmount = amount;
        Container = owner;

        _id = Guid.NewGuid().ToString().ToUpper();

        if(amount > item.MaxStackSize)
        {
            throw new ArgumentException("Can't have a larger stack than the item will allow");
        }
    }
    
    public string ID { get { return _id; } }
    public ItemBase Item { get; private set; }
    public int ItemAmount { get; private set; }
    public IContainerBase Container { get; private set; }
    public bool IsFull { get { return ItemAmount >= Item.MaxStackSize; } }

    public event Action OnUpdate;

    private readonly string _id;

    private Dictionary<string, object> RuntimeData
    {
        get
        {
            if(_runtimeData == null)
            {
                _runtimeData = new Dictionary<string, object>();
            }

            return _runtimeData;
        }
    }
    private Dictionary<string, object> _runtimeData;

    public T GetRuntimeDataUnsafe<T>(string key)
    {
        return (T)RuntimeData[key];
    }
    public T GetRuntimeData<T>(string key)
    {
        if (!RuntimeData.ContainsKey(key))
        {
            SetRuntimeData(key, default(T));
        }

        return (T)RuntimeData[key];
    }
    public void SetRuntimeData(string key, object value)
    {
        if (!RuntimeData.ContainsKey(key))
        {
            RuntimeData.Add(key, value);
        }
        else
        {
            RuntimeData[key] = value;
        }
    }
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
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        
        if(obj is ItemStack)
        {
            ItemStack otherStack = (ItemStack)obj;

            return otherStack._id == this._id;
        }

        return false;
    }
    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
    public override string ToString()
    {
        return string.Format("Stack({0}) containing {1} owned by {2}", _id, Item, Container);
    }
}
