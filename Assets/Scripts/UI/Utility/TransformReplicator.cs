using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransformReplicator : MonoBehaviour, ILayoutSelfController
{
    [SerializeField]
    private Vector2 sizeOffset;
    [SerializeField]
    private Vector2 positionOffset;

    public RectTransform Target;

    private RectTransform rectTransform { get { return (RectTransform)transform; } }

    private void Start()
    {
        SetLayout();
    }
    private void OnEnable()
    {
        SetLayout();
    }
    private void OnValidate()
    {
        SetLayout();
    }
    private void SetLayout()
    {
        if (Target == null)
            return;

        SetLayoutHorizontal();
        SetLayoutVertical();
    }
    public void SetLayoutHorizontal()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Target);

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Target.GetWorldRect().width + sizeOffset.x);
        rectTransform.pivot = Target.pivot;
        transform.position = Target.transform.position + (Vector3)positionOffset;
    }
    public void SetLayoutVertical()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Target);

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Target.GetWorldRect().height + sizeOffset.y);
    }
}
