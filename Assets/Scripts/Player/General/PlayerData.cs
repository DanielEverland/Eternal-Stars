using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData.asset", menuName = "Creature/Player Data", order = Utility.CREATE_ASSET_ORDER_ID)]
public class PlayerData : CreatureData {

    public int InventorySize { get { return _defaultInventorySize; } }

    [SerializeField]
    private int _defaultInventorySize = 32;
    [SerializeField]
    private DefaultInventoryItem[] _defaultInventoryItems;

    public override void AssignData(Creature creature)
    {
        base.AssignData(creature);

        if(creature is Player)
        {
            Player player = (Player)creature;

            for (int i = 0; i < _defaultInventoryItems.Length; i++)
            {
                DefaultInventoryItem entry = _defaultInventoryItems[i];

                if(entry.Item.MaxStackSize < entry.Amount)
                {
                    int amount = entry.Amount;

                    while (amount > 0)
                    {
                        if(entry.Item.MaxStackSize < amount)
                        {
                            player.ItemContainer.Add(entry.Item, entry.Item.MaxStackSize);

                            amount -= entry.Item.MaxStackSize;
                        }
                        else
                        {
                            player.ItemContainer.Add(entry.Item, amount);

                            break;
                        }
                    }
                }
                else
                {
                    player.ItemContainer.Add(entry.Item, entry.Amount);
                }
            }
        }
    }

    [System.Serializable]
    private class DefaultInventoryItem
    {
        public ItemBase Item;
        public int Amount = 1;
    }
}
