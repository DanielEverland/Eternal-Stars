using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Shake {

    [SerializeField]
    private float DegredationSpeed = 10;

    private float MaxPower;

    private static CameraShake Instance;

    protected override void Awake()
    {
        base.Awake();

        MaxPower = Power;
        Power = 0;
        Instance = this;
    }
    protected override void Update()
    {
        base.Update();

        Power = Mathf.Lerp(Power, 0, DegredationSpeed * Time.unscaledDeltaTime);
    }
    public static void Shake(float? power = null)
    {
        if (!power.HasValue)
        {
            Instance.Power = Instance.MaxPower;
        }
        else
        {
            Instance.Power = power.Value;
        }
    }
}
