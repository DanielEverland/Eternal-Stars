using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SlotBase : MonoBehaviour {
    
    public static SlotBase SelectedSlot { get; private set; }

    public object Index { get; private set; }
    public IContainerBase Container { get; private set; }

    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private bool containsMouse = false;

    private Rect oldLocalRect;
    private Rect worldRect;
    private bool shouldForceFitIcons;

    public void Initialize(IContainerBase container, object index, bool shouldForceFitIcons = false)
    {
        this.shouldForceFitIcons = shouldForceFitIcons;
        this.Container = container;
        this.Index = index;
    }
    public void AssignIcon(ItemIconElement iconElement)
    {
        RectTransform iconRectTransform = (RectTransform)iconElement.transform;
        RectTransform slotRectTransform = (RectTransform)transform;

        if(shouldForceFitIcons)
        {
            iconRectTransform.anchorMin = Vector2.zero;
            iconRectTransform.anchorMax = Vector2.one;
            iconRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRectTransform.rect.width);
            iconRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRectTransform.rect.height);
        }
        else
        {
            iconRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            iconRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        }
        
        iconElement.Stack.ChangeContainer(Container);
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
