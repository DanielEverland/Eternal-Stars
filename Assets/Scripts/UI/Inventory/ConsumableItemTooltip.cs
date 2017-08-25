using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConsumableItemTooltip : CustomTooltipLoadout {
    
    private ConsumableItem ConsumableItem { get { return (ConsumableItem)Item; } }
    
    protected override void CreateLayout()
    {
        for (int i = 0; i < ConsumableItem.OnConsumeActions.Count; i++)
        {
            ItemAction itemAction = ConsumableItem.OnConsumeActions[i];

            TMP_Text textElement = PlayModeObjectPool.Pool.GetObject("ItemTooltipContentElement").GetComponent<TMP_Text>();
            textElement.transform.SetParent(Tooltip.contentParent);

            textElement.text = "Use: " + itemAction.Description;
        }
    }
}
