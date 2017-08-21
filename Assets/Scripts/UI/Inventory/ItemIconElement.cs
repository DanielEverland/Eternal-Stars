using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIconElement : MonoBehaviour {

    [SerializeField]
    private Image Icon;
    [SerializeField]
    private Text AmountLabel;

    private RectTransform rectTransform { get { return (RectTransform)transform; } }
    
    public void Initialize(ItemStack stack)
    {
        stack.Container.OnContainerChanged += Terminate;

        rectTransform.sizeDelta = new Vector2(InventoryBase.ELEMENT_SIZE * stack.Item.InventorySize.x, InventoryBase.ELEMENT_SIZE * stack.Item.InventorySize.y);
        Icon.sprite = stack.Item.Icon;
    }
    private void Terminate()
    {
        PlayModeObjectPool.Pool.ReturnObject(gameObject);
    }
}
