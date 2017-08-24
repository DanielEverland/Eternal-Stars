using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemTooltip : BaseInventoryItemTooltip {

    private List<GameObject> ObjectsToReturn;

    protected override void AssignValuesToUI(ItemBase item)
    {
        base.AssignValuesToUI(item);

        ConsumableItem consumableItem = (ConsumableItem)item;

        AssignValues(consumableItem);
    }
    protected virtual void AssignValues(ConsumableItem item)
    {
        ObjectsToReturn = new List<GameObject>();

        for (int i = 0; i < item.OnConsumeActions.Count; i++)
        {
            ItemAction itemAction = item.OnConsumeActions[i];

            TMP_Text textElement = PlayModeObjectPool.Pool.GetObject("ItemTooltipContentElement").GetComponent<TMP_Text>();
            textElement.transform.SetParent(contentParent);

            textElement.text = "Use: " + itemAction.Description;

            ObjectsToReturn.Add(textElement.gameObject);
        }
    }
    private void OnReturned()
    {
        for (int i = 0; i < ObjectsToReturn.Count; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(ObjectsToReturn[i]);
        }
    }
}
