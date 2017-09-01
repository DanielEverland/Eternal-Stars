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
    [SerializeField]
    private ColorSwatch ColorSwatch;

    private RectTransform rectTransform { get { return (RectTransform)transform; } }
    private ContainerBase playerContainer { get { return Player.Instance.ItemContainer; } }

    private ItemStack stack;
    private Action updateCallback;

    private bool containsMouse;
    private bool dragging;
    
    public void Initialize(ItemStack stack, Action updateCallback)
    {
        this.stack = stack;
        this.updateCallback = updateCallback;

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

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                DoRightClick();
            }
        }

        DoAnimation();
        DoDragging();
    }
    private void DoAnimation()
    {
        if (containsMouse)
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
    private void DoDragging()
    {
        if (containsMouse && !dragging)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                dragging = true;

                transform.SetParent(Canvas2D.Static);
            }
        }
        else if (dragging)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
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

                if(updateCallback != null)
                    updateCallback.Invoke();
            }
            else
            {
                transform.position = Input.mousePosition;
                transform.SetSiblingIndex(transform.parent.childCount - 1);

                EG_Input.SuppressInput();
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
