using UnityEngine.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingEffect : MonoBehaviour {

    public static PostProcessingProfile Profile { get; private set; }

    [SerializeField]
    private PostProcessingProfile profile;
    [SerializeField]
    private Creature player;

    private const float EFFECT_THRESHOLD = 0.2f;

    private const float COLORGRADING_MIN = 0.5f;
    private const float COLORGRADING_MAX = 1;

    private float oldHealth;    
    
    private void Awake()
    {
        Profile = Instantiate(profile);
    }
    private void Update()
    {
        if(oldHealth != player.Health)
        {
            UpdateProfile();
        }

        oldHealth = player.Health;
    }
    private void UpdateProfile()
    {
        float percentage = Mathf.Clamp(player.Health / player.MaxHealth / EFFECT_THRESHOLD, 0, 1);

        AssignValue(percentage);
    }
    private void AssignValue(float percentage)
    {
        SetSaturation(percentage);
    }
    private void SetSaturation(float percentage)
    {
        profile.colorGrading.enabled = percentage != 1;

        ColorGradingModel.Settings settings = Profile.colorGrading.settings;
        
        settings.basic.saturation = COLORGRADING_MIN + (COLORGRADING_MAX - COLORGRADING_MIN) * percentage;

        Profile.colorGrading.settings = settings;
    }
}
