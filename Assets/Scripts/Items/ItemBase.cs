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
}