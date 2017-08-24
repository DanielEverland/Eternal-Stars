using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestoreHealthAction.asset", menuName = "Items/Actions/Restore Health", order = Utility.CREATE_ASSET_ORDER_ID)]
public class RestoreHealthAction : ItemAction {

    [SerializeField]
    private float HealthPointsToRestore;

    public override string Description { get { return string.Format("Restores " + HealthPointsToRestore + " to player"); } }

    public override void Action()
    {
        throw new NotImplementedException("Restore health to player here");
    }
}
