using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumableItem.asset", menuName = "Items/Consumable", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ConsumableItem : ItemBase {

    [SerializeField]
    private ItemAction OnConsume;

    public void Consume()
    {
        OnConsume.Action();

        throw new NotImplementedException("Add a destroy item feature here");
    }
}
