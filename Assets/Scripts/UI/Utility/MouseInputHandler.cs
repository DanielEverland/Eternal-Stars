using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour {

    public event UnityAction OnMouseEnter;
    public event UnityAction OnMouseExit;
    public event UnityAction OnMouseStay;

    public bool ContainsMouse { get; set; }

    private RectTransform rectTransform { get { return (RectTransform)transform; } }
    
    private void Update()
    {
        bool containsMouseThisFrame = rectTransform.GetWorldRect().Contains(Input.mousePosition);

        if(ContainsMouse && containsMouseThisFrame)
        {
            MouseStay();
        }
        else if(!ContainsMouse && containsMouseThisFrame)
        {
            MouseEnter();
        }
        else if(ContainsMouse && !containsMouseThisFrame)
        {
            MouseExit();
        }
    }
    private void MouseStay()
    {
        if (OnMouseStay != null)
            OnMouseStay.Invoke();
    }
    private void MouseEnter()
    {
        ContainsMouse = true;

        if (OnMouseEnter != null)
            OnMouseEnter.Invoke();
    }
    private void MouseExit()
    {
        ContainsMouse = false;

        if (OnMouseExit != null)
            OnMouseExit.Invoke();
    }
}
