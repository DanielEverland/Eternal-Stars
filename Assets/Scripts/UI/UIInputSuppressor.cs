using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(RectTransform))]
public class UIInputSuppressor : MonoBehaviour
{
    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private void Update()
    {
        if (rectTransform.GetWorldRect().Contains(Input.mousePosition))
            EG_Input.SuppressInput();
    }
}
