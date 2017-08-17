using UnityEngine.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingEffect : MonoBehaviour {

    public static PostProcessingProfile Profile { get; private set; }

    [SerializeField]
    private PostProcessingProfile templateProfile;
    

    private Creature Player { get { return PlayModeManager.Player; } }

    private const float EFFECT_THRESHOLD = 0.3f;

    private const float COLORGRADING_MIN = 0.5f;
    private const float COLORGRADING_MAX = 1;

    private const float VIGNETTE_MIN = 0.1f;
    private const float VIGNETTE_MAX = 0.4f;

    private const float CHROMATIC_ABERRATION_MIN = 0f;
    private const float CHROMATIC_ABERRATION_MAX = 0.4f; 

    private float oldHealth;    
    
    private void Awake()
    {
        Profile = Instantiate(templateProfile);
    }
    private void Update()
    {
        if(oldHealth != Player.Health)
        {
            UpdateProfile();
        }

        oldHealth = Player.Health;
    }
    private void UpdateProfile()
    {
        float percentage = Mathf.Clamp(Player.Health / Player.MaxHealth / EFFECT_THRESHOLD, 0, 1);

        AssignValue(percentage);
    }
    private void AssignValue(float percentage)
    {
        SetSaturation(percentage);
        SetVignette(percentage);
        SetChromaticAberration(percentage);
    }
    private void SetSaturation(float percentage)
    {
        ColorGradingModel.Settings settings = Profile.colorGrading.settings;
        
        settings.basic.saturation = COLORGRADING_MIN + (COLORGRADING_MAX - COLORGRADING_MIN) * percentage;

        Profile.colorGrading.settings = settings;
    }
    private void SetVignette(float percentage)
    {
        Profile.vignette.enabled = percentage != 1;

        VignetteModel.Settings settings = Profile.vignette.settings;

        float invertedPercentage = 1 - percentage;
        settings.intensity = VIGNETTE_MIN + (VIGNETTE_MAX - VIGNETTE_MIN) * invertedPercentage;

        Profile.vignette.settings = settings;
    }
    private void SetChromaticAberration(float percentage)
    {
        Profile.chromaticAberration.enabled = percentage != 1;

        ChromaticAberrationModel.Settings settings = Profile.chromaticAberration.settings;

        float invertedPercentage = 1 - percentage;
        settings.intensity = CHROMATIC_ABERRATION_MIN + (CHROMATIC_ABERRATION_MAX - CHROMATIC_ABERRATION_MIN) * invertedPercentage;

        Profile.chromaticAberration.settings = settings;
    }
}
