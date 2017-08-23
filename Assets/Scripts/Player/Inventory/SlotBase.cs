using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlotBase : MonoBehaviour {
    
    public static SlotBase SelectedSlot { get; private set; }

    public Vector2 Index { get; private set; }

    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private InventoryBase inventory;
    private bool containsMouse = false;

    private Rect oldLocalRect;
    private Rect worldRect;

    public void Initialize(InventoryBase owner, Vector2 index)
    {
        this.inventory = owner;
        this.Index = index;
    }
    public void AssignIcon(ItemIconElement iconElement)
    {
        RectTransform iconRectTransform = (RectTransform)iconElement.transform;
        RectTransform slotRectTransform = (RectTransform)transform;

        iconRectTransform.anchoredPosition = slotRectTransform.anchoredPosition;
    }
    private void Update()
    {
        CheckWorldRect();
        
        if (containsMouse && !worldRect.Contains(Input.mousePosition))
        {
            if (SelectedSlot == this)
                SelectedSlot = null;
        }
        else if(!containsMouse && worldRect.Contains(Input.mousePosition))
        {
            SelectedSlot = this;
        }
    }
    private void CheckWorldRect()
    {
        if(oldLocalRect != rectTransform.rect)
        {
            oldLocalRect = rectTransform.rect;

            worldRect = rectTransform.GetWorldRect();
        }
    }
}
