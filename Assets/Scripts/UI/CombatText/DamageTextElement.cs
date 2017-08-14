using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextElement : CombatTextBase<float> {

    [SerializeField]
    private float duration = 1;
    [SerializeField]
    private bool lerpValue = true;

    private float textToDisplay;
    private float time;

    private const float LERP_SPEED = 10;
    
    protected override void Update()
    {
        base.Update();

        LerpText();
        CheckIfDestroy();
    }
    private void CheckIfDestroy()
    {
        time += Time.unscaledDeltaTime;

        if(time >= duration)
        {
            Destroy(gameObject);
        }
    }
    private void LerpText()
    {
        if (lerpValue)
        {
            textToDisplay = Mathf.CeilToInt(Mathf.Lerp(textToDisplay, Value, LERP_SPEED * Time.unscaledDeltaTime));

            textElement.text = textToDisplay.ToString();
        }
        else
        {
            textElement.text = Value.ToString();
        }        
    }
    public void AddDamage(float damageToAdd, Vector3 position)
    {
        UpdateText(Value + damageToAdd, position);
    }
    public override void UpdateText(float newValue, Vector3 position)
    {
        textElement.text = newValue.ToString();
        Value = newValue;

        this.TextWorldPosition = position;
    }
}
