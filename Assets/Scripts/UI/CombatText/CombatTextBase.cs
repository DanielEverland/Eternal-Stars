using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public abstract class CombatTextBase<T> : MonoBehaviour {

    [SerializeField]
    protected TMP_Text textElement;
    
    public T Value { get; protected set; }

    protected RectTransform rectTransform { get { return (RectTransform)transform; } }
    protected Vector3 TextWorldPosition { get; set; }
    protected Vector2 Offset { get; set; }

    private Vector2 oldPosition;
    private LerpOffset lerpMotor;

    protected virtual void Awake()
    {
        lerpMotor = GetComponent<LerpOffset>();
    }
    protected virtual void Start()
    {
        oldPosition = rectTransform.position;
    }
    public virtual void UpdateText(T newValue, Vector3 position)
    {
        textElement.text = newValue.ToString();
        Value = newValue;

        this.TextWorldPosition = position;
    }
    protected virtual void Update()
    {
        SetPosition(TextWorldPosition);
    }
    protected virtual void SetPosition(Vector3 worldPos)
    {
        Offset += (Vector2)rectTransform.position - oldPosition;

        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);

        if(lerpMotor != null)
            lerpMotor.SetAnchor(screenPoint);

        rectTransform.position = screenPoint + Offset;
        oldPosition = rectTransform.position;
    }
}
