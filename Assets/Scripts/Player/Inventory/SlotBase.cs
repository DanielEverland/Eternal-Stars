using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotBase : MonoBehaviour {
    
    public void Initialize(InventoryBase owner)
    {

    }
    public void AssignIcon(ItemIconElement iconElement)
    {
        RectTransform iconRectTransform = (RectTransform)iconElement.transform;
        RectTransform slotRectTransform = (RectTransform)transform;

        iconRectTransform.anchoredPosition = slotRectTransform.anchoredPosition;
    }
}
