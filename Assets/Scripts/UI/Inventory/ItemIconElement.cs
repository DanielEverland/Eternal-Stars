using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemIconElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private Image Icon;
    [SerializeField]
    private TMP_Text AmountLabel;
    [SerializeField]
    private Graphic FrameGraphic;

    private RectTransform rectTransform { get { return (RectTransform)transform; } }
    private ContainerBase playerContainer { get { return Player.Instance.Container; } }

    private ItemStack stack;
    private InventoryBase inventoryBase;

    private bool containsMouse;
    private bool dragging;
    
    public void Initialize(ItemStack stack, InventoryBase inventory)
    {
        this.stack = stack;
        this.inventoryBase = inventory;

        dragging = false;
        containsMouse = false;

        stack.OnUpdate += SetProperties;

        SetProperties();
    }
    private void SetProperties()
    {
        SetSize();
        SetIcon();
        SetRarityColor();
        SetAmountLabel();
    }
    private void Update()
    {
        if (containsMouse)
        {
            InventoryItemTooltipManager.Tick(stack.Item);

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                DoRightClick();
            }
        }

        DoDragging();
    }
    private void DoDragging()
    {
        if (containsMouse && !dragging)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                dragging = true;
            }
        }
        else if (dragging)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                dragging = false;

                if (SlotBase.SelectedSlot != null)
                {
                    if (playerContainer.Fits(stack.Item, SlotBase.SelectedSlot.Index))
                    {
                        Player.Instance.Container.Remove(stack);
                        Player.Instance.Container.Add(SlotBase.SelectedSlot.Index, stack);
                    }
                }

                inventoryBase.Refresh();
            }
            else
            {
                transform.position = Input.mousePosition;
            }
        }
    }
    public void DoRightClick()
    {
        stack.Item.OnRightClick(stack);
    }
    private void SetIcon()
    {
        Icon.sprite = stack.Item.Icon;
    }
    private void SetRarityColor()
    {
        FrameGraphic.color = stack.Item.Rarity.Color;
    }
    private void SetSize()
    {
        rectTransform.sizeDelta = new Vector2()
        {
            x = stack.Item.InventorySize.x * InventoryBase.ELEMENT_SIZE + ((stack.Item.InventorySize.x - 1) * InventoryBase.ELEMENT_SPACING),
            y = stack.Item.InventorySize.y * InventoryBase.ELEMENT_SIZE + ((stack.Item.InventorySize.y - 1) * InventoryBase.ELEMENT_SPACING),
        };
    }
    private void SetAmountLabel()
    {
        AmountLabel.text = (stack.ItemAmount > 1) ? stack.ItemAmount.ToString() : "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        containsMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        containsMouse = false;
    }
    private void OnDestroy()
    {
        if(stack != null)
            stack.OnUpdate -= SetProperties;
    }
}
