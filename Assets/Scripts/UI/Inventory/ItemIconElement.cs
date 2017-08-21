using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconElement : MonoBehaviour {

    [SerializeField]
    private Image Icon;
    [SerializeField]
    private TMP_Text AmountLabel;
    [SerializeField]
    private Graphic FrameGraphic;

    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private ItemStack stack;
    private InventoryBase inventoryBase;
    
    public void Initialize(ItemStack stack, InventoryBase inventory)
    {
        this.stack = stack;
        this.inventoryBase = inventory;

        SetSize();
        SetIcon();
        SetRarityColor();
        SetAmountLabel();
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
}
