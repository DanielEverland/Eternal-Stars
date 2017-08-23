using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class ImageRatioSustainer : MonoBehaviour, ILayoutSelfController
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private bool _alignToParent = true;

    private bool WidthIsSmallest { get { return _image.sprite.rect.width < _image.sprite.rect.height; } }
    private bool HeightIsSmallest { get { return _image.sprite.rect.width > _image.sprite.rect.height; } }
    private float HeightRatio { get { return _image.sprite.rect.height / _image.sprite.rect.width;} }
    private float WidthRatio { get { return _image.sprite.rect.width / _image.sprite.rect.height; } }
    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private bool hasReferences
    {
        get
        {
            return _image.sprite != null;
        }
    }
    
    private Vector2 AnchorSize
    {
        get
        {
            if (_alignToParent)
                return ((RectTransform)transform.parent).rect.size;

            return rectTransform.rect.size;
        }
    }

    public void SetLayoutHorizontal()
    {
        if (!hasReferences)
            return;

        if (HeightIsSmallest)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * WidthRatio, AnchorSize.y);
        }
    }
    public void SetLayoutVertical()
    {
        if (!hasReferences)
            return;

        if (WidthIsSmallest)
        {
            rectTransform.sizeDelta = new Vector2(AnchorSize.x, rectTransform.sizeDelta.x * HeightRatio);
        }
    }
    private void OnValidate()
    {
        _image = GetComponent<Image>();
    }
}
