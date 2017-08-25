using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryItemTooltipManager {
    
    private static ItemTooltip tooltipInstance;
    private static ItemBase currentlySelectItem;
    private static float timeSelected;

    private const float DELAY = 0.2f;
    
    public static void Tick(ItemBase item)
    {
        TickTooltip();
        PollNewItem(item);
        PollPushTooltip();
    }
    private static void TickTooltip()
    {
        if(tooltipInstance != null)
            tooltipInstance.Tick();
    }
    public static void DeleteItemTooltip()
    {
        PlayModeObjectPool.Pool.ReturnObject(tooltipInstance.gameObject);

        tooltipInstance = null;
        currentlySelectItem = null;
    }
    private static void PollNewItem(ItemBase item)
    {
        if (item != currentlySelectItem)
        {
            currentlySelectItem = item;
            timeSelected = Time.unscaledTime;

            if (tooltipInstance != null)
            {
                tooltipInstance.Initialize(item);
            }
        }
    }
    private static void PollPushTooltip()
    {
        if (tooltipInstance == null && Time.unscaledTime - timeSelected > DELAY)
        {
            CreateNewTooltip();
            tooltipInstance.Initialize(currentlySelectItem);
        }
    }
    private static void CreateNewTooltip()
    {
        tooltipInstance = PlayModeObjectPool.Pool.GetObject("ItemTooltip").GetComponent<ItemTooltip>();
        tooltipInstance.transform.SetParent(Canvas2D.Static.transform);
    }
}
