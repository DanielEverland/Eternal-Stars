using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RestoreHealthAction : ItemAction {

    [SerializeField]
    private float HealthPointsToRestore;

    public override void Action()
    {
        throw new NotImplementedException("Restore health to player here");
    }
}
