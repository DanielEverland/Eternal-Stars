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

    private static float LERP_SPEED = 10;

    private Vector2 oldPosition;
    private Vector2 oldSize;
    private Rect oldRect;
    private bool containsMouse;

    private Color targetColor;
    private Color currentColor;

    public void Initialize(ItemHandler itemHandler)
    {
        this.itemHandler = itemHandler;

        label.text = itemHandler.Item.Name;
        rectTransform.sizeDelta = Vector2.zero;

        ItemHandlerLabelManager.AddLabel(this);

        SetProperties(itemHandler.Item);
    }
    public void OnClick()
    {
        PlayerItemPickupManager.PickUpItem(itemHandler);
    }
    public void OnMouseEnter()
    {
        targetColor = Color.Lerp(itemHandler.Item.Rarity.Color, Color.white, 0.3f);
    }
    public void OnMouseExit()
    {
        targetColor = itemHandler.Item.Rarity.Color;
    }
    private void SetProperties(ItemBase item)
    {
        targetColor = item.Rarity.Color;
        currentColor = targetColor;

        ApplyColor(currentColor);
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

        Vector2 targetSize = label.rectTransform.sizeDelta + sizePadding;
        Vector2 lerpedSize = Vector2.Lerp(rectTransform.sizeDelta, targetSize, Time.unscaledDeltaTime * LERP_SPEED);
        lerpedSize.x = targetSize.x;

        rectTransform.sizeDelta = lerpedSize;

        if (itemHandler == null)
            return;

        rectTransform.position = (Vector2)Camera.main.WorldToScreenPoint(itemHandler.transform.position) + positionOffset;
        
        LerpColor();
    }
    private void LerpColor()
    {
        currentColor = Color.Lerp(currentColor, targetColor, LERP_SPEED * Time.unscaledDeltaTime);

        ApplyColor(currentColor);
    }
    private void ApplyColor(Color color)
    {
        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].color = color;
        }
    }
    private void RecalculateRect()
    {
        oldPosition = rectTransform.anchoredPosition;
        oldSize = rectTransform.sizeDelta;
        oldRect = new Rect(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
    }
}
