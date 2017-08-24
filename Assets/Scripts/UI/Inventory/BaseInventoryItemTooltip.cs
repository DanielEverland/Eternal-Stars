using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseInventoryItemTooltip : MonoBehaviour {

    [SerializeField]
    protected ConstrainedContentSizeFitter contentSizeFitter;
    [SerializeField]
    protected RectTransform header;
    [SerializeField]
    protected TMP_Text typeTextElement;
    [SerializeField]
    protected TMP_Text nameTextElement;
    [SerializeField]
    protected Image iconImage;
    [SerializeField]
    protected Graphic[] glowElements;
    [SerializeField]
    protected Graphic[] backgroundElements;
    [SerializeField]
    protected Graphic[] vanityElements;
    [SerializeField]
    protected RectTransform contentParent;
    
    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private int lastFrameTicked;

    private const float GLOW_ALPHA = 200f / 255f;
    private const float BACKGROUND_ALPHA = 180f / 255f;
    private const float ADDITIONAL_HEIGHT = 20;

    public void Initialize(ItemBase item)
    {
        AssignValuesToUI(item);

        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        contentSizeFitter.horizontalMinSize = nameTextElement.rectTransform.rect.width;

        DoLayout();
    }
    protected virtual void AssignValuesToUI(ItemBase item)
    {
        nameTextElement.text = item.Name;
        iconImage.sprite = item.Icon;
        typeTextElement.text = string.Format("{0} {1}", item.Rarity.Name, item.ItemType);

        Color glowColor = new Color(item.Rarity.Color.r, item.Rarity.Color.g, item.Rarity.Color.b, GLOW_ALPHA);
        for (int i = 0; i < glowElements.Length; i++)
        {
            glowElements[i].color = glowColor;
        }

        Color backgroundColor = Color.Lerp(item.Rarity.Color, Color.black, 0.4f);
        backgroundColor.a = BACKGROUND_ALPHA;
        for (int i = 0; i < backgroundElements.Length; i++)
        {
            backgroundElements[i].color = backgroundColor;
        }

        for (int i = 0; i < vanityElements.Length; i++)
        {
            vanityElements[i].color = item.Rarity.Color;
        }
    }
    private void DoLayout()
    {
        header.sizeDelta = new Vector2(Mathf.Max(contentParent.rect.width, header.sizeDelta.x), header.sizeDelta.y);

        rectTransform.sizeDelta = new Vector2()
        {
            x = header.sizeDelta.x,
            y = header.sizeDelta.y + contentParent.rect.height + ADDITIONAL_HEIGHT,
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
}
