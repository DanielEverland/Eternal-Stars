using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject {

    public Rarity Rarity { get { return _rarity; } }
    public string Name { get { return _name; } }

    [SerializeField]
    private Rarity _rarity;
    [SerializeField]
    private string _name;
}
