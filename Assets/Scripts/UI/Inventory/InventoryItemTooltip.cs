using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemTooltip : MonoBehaviour {
    
    [SerializeField]
    private TMP_Text textElement;
    [SerializeField]
    private RectTransform background;
    [SerializeField]
    private Graphic frame;

    private const float MAX_WIDTH = 150;
    private const float MIN_WIDTH = 50;

    private const float BACKGROUND_PADDING = 5;

    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private int lastFrameTicked;

    public void Initialize(ItemBase item)
    {
        textElement.text = item.GetTooltip();
        frame.color = item.Rarity.Color;
        
        LayoutTooltip();
    }
    private void LayoutTooltip()
    {
        LayoutText();
        LayoutFrame();
    }
    private void LayoutFrame()
    {
        background.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textElement.rectTransform.rect.width + BACKGROUND_PADDING * 2);
        background.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textElement.rectTransform.rect.height + BACKGROUND_PADDING * 2);

        background.position = textElement.rectTransform.position + new Vector3(-BACKGROUND_PADDING, -BACKGROUND_PADDING);
    }
    private void LayoutText()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(textElement.rectTransform);

        textElement.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(textElement.preferredWidth, MIN_WIDTH, MAX_WIDTH));

        LayoutRebuilder.ForceRebuildLayoutImmediate(textElement.rectTransform);

        textElement.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textElement.preferredHeight);
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
