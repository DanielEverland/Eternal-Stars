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
    public RectTransform footerParent;
    public TMP_Text typeTextElement;
    public TMP_Text nameTextElement;
    public Image iconImage;
    public Graphic[] glowElements;
    public Graphic[] backgroundElements;
    public Graphic[] vanityElements;
    public RectTransform[] elementsToConsiderForLayout;
    public RectTransform[] elementsToForceLayoutRebuild;
    public RectTransform contentParent;
    public CanvasGroup canvasGroup;
    
    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private int lastFrameTicked;
    private CustomTooltipLoadout tooltipLoadout;

    private const float GLOW_ALPHA = 200f / 255f;
    private const float BACKGROUND_ALPHA = 230f / 255f;
    private const float HEADER_OFFSET = 5;

    private bool shouldRelayout = false;

    public void Initialize(ItemBase item)
    {
        ReturnContent();

        Item = item;
        tooltipLoadout = item.TooltipLoadout;
        
        AssignValuesToUI();

        DoDefaultLoadout();

        if (tooltipLoadout != null)
            tooltipLoadout.Initialize(this);

        ForceLayoutRebuild();
        contentSizeFitter.horizontalMinSize = nameTextElement.rectTransform.rect.width;

        shouldRelayout = true;

        Move();
        Hide();
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
    private void DoDefaultLoadout()
    {
        string tooltipContent = Item.GetTooltipContent().Trim();
        string footerContent = Item.GetTooltipFooter().Trim();

        if (tooltipContent != "")
        {
            AddTextElement(contentParent, tooltipContent);
        }

        if(footerContent != "")
        {
            AddTextElement(footerParent, string.Format("<size=11>{0}</size>", footerContent));
        }
    }
    private void AddTextElement(RectTransform parent, string text)
    {
        TMP_Text textElement = PlayModeObjectPool.Pool.GetObject("ItemTooltipContentElement").GetComponent<TMP_Text>();
        textElement.transform.SetParent(parent);

        textElement.text = text;
    }
    private void ForceLayoutRebuild()
    {
        for (int i = 0; i < elementsToForceLayoutRebuild.Length; i++)
        {
            elementsToForceLayoutRebuild[i].gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(elementsToForceLayoutRebuild[i]);
        }
    }
    private void DoLayout()
    {
        header.anchoredPosition = new Vector2(header.anchoredPosition.x, -HEADER_OFFSET);

        float maxWidth = 0;
        Rect thisRect = rectTransform.GetWorldRect();

        for (int i = 0; i < elementsToConsiderForLayout.Length; i++)
        {
            Rect elementRect = elementsToConsiderForLayout[i].GetWorldRect();

            float width = elementRect.xMax - thisRect.xMin;

            if (width > maxWidth)
                maxWidth = width;
        }
        
        rectTransform.sizeDelta = new Vector2()
        {
            x = Mathf.Max(contentParent.rect.width, maxWidth),
            y = header.sizeDelta.y + footerParent.sizeDelta.y + contentParent.rect.height + HEADER_OFFSET,
        };

        footerParent.gameObject.SetActive(footerParent.sizeDelta.y > 10);
        contentParent.gameObject.SetActive(contentParent.sizeDelta.y > 10);

        Show();
    }
    public void Tick()
    {
        lastFrameTicked = Time.frameCount;
    }
    private void Update()
    {
        Move();

        if (shouldRelayout)
            DoLayout();

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
        ReturnContent();
    }
    private void ReturnContent()
    {
        for (int i = 0; i < contentParent.childCount; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(contentParent.GetChild(i).gameObject, false);
        }

        for (int i = 0; i < footerParent.childCount; i++)
        {
            PlayModeObjectPool.Pool.ReturnObject(footerParent.GetChild(i).gameObject, false);
        }

        if (tooltipLoadout != null)
            tooltipLoadout.OnReturned();
    }
    private void Hide()
    {
        canvasGroup.alpha = 0;
    }
    private void Show()
    {
        canvasGroup.alpha = 1;
    }
}
