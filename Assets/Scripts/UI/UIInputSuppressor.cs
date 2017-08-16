using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInputSuppressor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool containsMouse;

    private void Update()
    {
        if (containsMouse)
            EG_Input.SuppressInput();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        containsMouse = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        containsMouse = false;
    }
}
