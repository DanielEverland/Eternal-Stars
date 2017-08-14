using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item.asset", menuName = "Items/TestItem", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ItemBase : ScriptableObject {

    public Rarity Rarity { get { return _rarity; } }

    [SerializeField]
    private Rarity _rarity;
}
