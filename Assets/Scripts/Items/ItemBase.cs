using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject {

    public Rarity Rarity { get { return _rarity; } }

    [SerializeField]
    private Rarity _rarity;
}
