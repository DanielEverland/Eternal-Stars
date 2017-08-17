using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour {

    [SerializeField]
    private Image slider;
    [SerializeField]
    private Image backSlider;
    [SerializeField]
    private RectTransform indicator;
    
    private Player Player { get { return PlayModeManager.Player; } }

    private float targetFill;
    private float backsliderTarget = 1;
    private float oldHealthValue;
    private float backSliderCurrentWaitTime;

    private const float LERP_SPEED = 20;
    private const float BACK_SLIDER_WAIT_TIME = 0.5f;

    private void Update()
    {
        CheckBacksliderTime();
        UpdateInterface();

        oldHealthValue = Player.Health;
    }
    private void CheckBacksliderTime()
    {
        if (oldHealthValue != Player.Health)
        {
            backSliderCurrentWaitTime = BACK_SLIDER_WAIT_TIME;
        }
        else
        {
            backSliderCurrentWaitTime -= Time.unscaledDeltaTime;

            if(backSliderCurrentWaitTime < 0)
            {
                backsliderTarget = targetFill;
            }
        }
    }
    private void UpdateInterface()
    {
        targetFill = Player.Health / Player.MaxHealth;
        
        slider.fillAmount = Mathf.Lerp(slider.fillAmount, targetFill, LERP_SPEED * Time.unscaledDeltaTime);
        backSlider.fillAmount = Mathf.Lerp(backSlider.fillAmount, backsliderTarget, LERP_SPEED * Time.unscaledDeltaTime);

        float indicatorOffset = (1 - slider.fillAmount) * slider.rectTransform.rect.width;
        indicator.anchoredPosition = new Vector2(-indicatorOffset, 0);
    }
}
