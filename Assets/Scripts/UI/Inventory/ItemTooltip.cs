using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltip : MonoBehaviour {

    public ItemBase Item { get; private set; }
    
    public ConstrainedContentSizeFitter contentSizeFitter;
    public RectTransform header;
    public TMP_Text typeTextElement;
    public TMP_Text nameTextElement;
    public Image iconImage;
    public Graphic[] glowElements;
    public Graphic[] backgroundElements;
    public Graphic[] vanityElements;
    public RectTransform contentParent;
    
    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private int lastFrameTicked;
    private CustomTooltipLoadout tooltipLoadout;

    private const float GLOW_ALPHA = 200f / 255f;
    private const float BACKGROUND_ALPHA = 230f / 255f;
    private const float HEADER_OFFSET = 5;

    public void Initialize(ItemBase item)
    {
        Item = item;
        tooltipLoadout = item.TooltipLoadout;

        AssignValuesToUI();

        if (tooltipLoadout != null)
            tooltipLoadout.Initialize(this);

        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        contentSizeFitter.horizontalMinSize = nameTextElement.rectTransform.rect.width;

        DoLayout();
    }
    protected virtual void AssignValuesToUI()
    {
        nameTextElement.text = Item.Name;
        iconImage.sprite = Item.Icon;
        typeTextElement.text = string.Format("{0} {1}", Item.Rarity.Name, Item.ItemType);

        Color glowColor = new Color(Item.Rarity.Color.r, Item.Rarity.Color.g, Item.Rarity.Color.b, GLOW_ALPHA);
        for (int i = 0; i < glowElements.Length; i++)
        {
            glowElements[i].color = glowColor;
        }

        Color backgroundColor = Color.Lerp(Item.Rarity.Color, Color.black, 0.4f);
        backgroundColor.a = BACKGROUND_ALPHA;
        for (int i = 0; i < backgroundElements.Length; i++)
        {
            backgroundElements[i].color = backgroundColor;
        }

        for (int i = 0; i < vanityElements.Length; i++)
        {
            vanityElements[i].color = Item.Rarity.Color;
        }
    }
    private void DoLayout()
    {
        header.anchoredPosition = new Vector2(header.anchoredPosition.x, -HEADER_OFFSET);

        rectTransform.sizeDelta = new Vector2()
        {
            x = Mathf.Max(contentParent.rect.width, header.sizeDelta.x),
            y = header.sizeDelta.y + contentParent.rect.height + HEADER_OFFSET,
        };
    }
    public void Tick()
    {
        lastFrameTicked = Time.frameCount;
    }
    private void Update()
    {
        Move();
        
        PollDestroy();
    }
    private void Move()
    {
        transform.position = Input.mousePosition;
    }
    private void PollDestroy()
    {
        if(Time.frameCount - lastFrameTicked > 1)
        {
            InventoryItemTooltipManager.DeleteItemTooltip();
        }
    }
    private void OnReturned()
    {
        for (int i = 0; i < contentParent.childCount; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(contentParent.GetChild(i).gameObject);
        }

        if (tooltipLoadout != null)
            tooltipLoadout.OnReturned();
    }
}
