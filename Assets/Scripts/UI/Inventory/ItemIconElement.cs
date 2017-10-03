using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemIconElement : MonoBehaviour {

    [SerializeField]
    private Image Icon;
    [SerializeField]
    private TMP_Text AmountLabel;
    [SerializeField]
    private Graphic FrameGraphic;
    [SerializeField]
    private ColorSwatch ColorSwatch;
    [SerializeField]
    private MouseInputHandler InputHandler;

    public ItemStack Stack { get { return stack; } }

    private RectTransform rectTransform { get { return (RectTransform)transform; } }
    private InventoryContainer playerContainer { get { return Player.Instance.ItemContainer; } }

    private ItemStack stack;
    private Action updateCallback;

    private bool ContainsMouse { get { return InputHandler.ContainsMouse; } }
    private bool dragging;
    
    public void Initialize(ItemStack stack, Action updateCallback)
    {
        this.stack = stack;
        this.updateCallback = updateCallback;

        dragging = false;

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
        if (ContainsMouse && !dragging)
        {
            InventoryItemTooltipManager.Tick(stack.Item);

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                DoRightClick();
            }
        }

        DoAnimation();
        PollDragging();
    }
    private void DoAnimation()
    {
        if (ContainsMouse)
        {
            if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                StartColorTween(ColorSwatch.pressedColor);
            }
            else
            {
                StartColorTween(ColorSwatch.highlightedColor);
            }
        }
        else
        {
            StartColorTween(ColorSwatch.normalColor);
        }
    }
    private void StartColorTween(Color targetColor)
    {
        Icon.CrossFadeColor(targetColor, ColorSwatch.fadeDuration, true, true);
    }
    private void PollDragging()
    {
        if (ContainsMouse && !dragging)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartDragging();
            }
        }
        else if (dragging)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                StopDragging();
            }
            else
            {
                DoDragging();
            }
        }
    }
    private void StartDragging()
    {
        dragging = true;

        transform.SetParent(Canvas2D.Static);
        rectTransform.SetSize(Stack.Item.InventorySize * InventoryBase.SLOT_SIZE);
    }
    private void StopDragging()
    {
        dragging = false;

        if (SlotBase.SelectedSlot != null)
        {
            if (SlotBase.SelectedSlot.Container.Fits(SlotBase.SelectedSlot.Index, stack.Item))
            {
                stack.Container.Remove(stack);
                SlotBase.SelectedSlot.Container.Add(SlotBase.SelectedSlot.Index, stack);
            }
        }

        if (updateCallback != null)
            updateCallback.Invoke();
    }
    private void DoDragging()
    {
        transform.position = Input.mousePosition;
        transform.SetSiblingIndex(transform.parent.childCount - 1);

        EG_Input.SuppressInput();
    }
    public void DoRightClick()
    {
        stack.Item.CallRightClick(stack);
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
            x = stack.Item.InventorySize.x * InventoryBase.SLOT_SIZE + ((stack.Item.InventorySize.x - 1) * InventoryBase.ELEMENT_SPACING),
            y = stack.Item.InventorySize.y * InventoryBase.SLOT_SIZE + ((stack.Item.InventorySize.y - 1) * InventoryBase.ELEMENT_SPACING),
        };
    }
    private void SetAmountLabel()
    {
        AmountLabel.text = (stack.ItemAmount > 1) ? stack.ItemAmount.ToString() : "";
    }
    private void OnDestroy()
    {
        if(stack != null)
            stack.OnUpdate -= SetProperties;
    }
}
