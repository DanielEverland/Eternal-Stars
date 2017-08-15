using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : ScriptableObject {

    public virtual Rarity Rarity { get { return _rarity; } }
    public virtual string Name { get { return _name; } }

    [SerializeField]
    private Rarity _rarity;
    [SerializeField]
    private string _name;
}
