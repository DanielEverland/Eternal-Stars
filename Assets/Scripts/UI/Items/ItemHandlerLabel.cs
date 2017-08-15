using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class ItemHandlerLabel : MonoBehaviour {
    
    public RectTransform rectTransform { get { return (RectTransform)transform; } }
    public Rect Rect
    {
        get
        {
            if (rectTransform.sizeDelta != oldSize || rectTransform.anchoredPosition != oldPosition)
                RecalculateRect();

            return oldRect;
        }
    }

    [SerializeField]
    private TMP_Text label;
    [SerializeField]
    private Vector2 sizePadding;
    [SerializeField]
    private Vector2 positionOffset;
    [SerializeField]
    private Graphic[] graphics;
    
    private ItemHandler itemHandler;

    private Vector2 oldPosition;
    private Vector2 oldSize;
    private Rect oldRect;

    public void Initialize(ItemHandler itemHandler)
    {
        this.itemHandler = itemHandler;

        label.text = itemHandler.Item.Name;

        ItemHandlerLabelManager.AddLabel(this);

        SetProperties(itemHandler.Item);
    }
    private void SetProperties(ItemBase item)
    {
        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].color = item.Rarity.Color;
        }
    }
    public void Return()
    {
        ItemHandlerLabelManager.RemoveLabel(this);

        PlayModeObjectPool.Pool.ReturnObject(gameObject);
    }
    private void Update()
    {
        if (label == null)
            return;

        rectTransform.sizeDelta = label.rectTransform.sizeDelta + sizePadding;

        if (itemHandler == null)
            return;

        rectTransform.position = (Vector2)Camera.main.WorldToScreenPoint(itemHandler.transform.position) + positionOffset;
    }
    private void RecalculateRect()
    {
        oldPosition = rectTransform.anchoredPosition;
        oldSize = rectTransform.sizeDelta;
        oldRect = new Rect(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
    }
}
