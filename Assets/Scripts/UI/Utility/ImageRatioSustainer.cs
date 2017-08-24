using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AspectRatioFitter))]
public class ImageRatioSustainer : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private AspectRatioFitter _ratioFitter;

    private bool FieldsAreValid
    {
        get
        {
            if (_image == null)
                return false;

            return _image.sprite != null && _ratioFitter != null;
        }
    }
    private void Update()
    {
        if(FieldsAreValid)
            _ratioFitter.aspectRatio = _image.sprite.rect.width / _image.sprite.rect.height;
    }
}
