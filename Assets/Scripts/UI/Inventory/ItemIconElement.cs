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
    
    public void Initialize(ItemStack stack)
    {
        rectTransform.sizeDelta = new Vector2(InventoryBase.ELEMENT_SIZE * stack.Item.InventorySize.x, InventoryBase.ELEMENT_SIZE * stack.Item.InventorySize.y);
        Icon.sprite = stack.Item.Icon;
        FrameGraphic.color = stack.Item.Rarity.Color;

        SetAmountLabel(stack);
    }
    private void SetAmountLabel(ItemStack stack)
    {
        AmountLabel.text = (stack.ItemAmount > 1) ? stack.ItemAmount.ToString() : "";
    }
}
