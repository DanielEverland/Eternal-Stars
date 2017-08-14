using UnityEngine.PostProcessing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePostProcessingBehaviour : PostProcessingBehaviour {
    
    private void Start()
    {
        if(Application.isPlaying)
            profile = DyingEffect.Profile;
    }
}
