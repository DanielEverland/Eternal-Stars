using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestoreHealthAction.asset", menuName = "Items/Actions/Restore Health", order = Utility.CREATE_ASSET_ORDER_ID)]
public class RestoreHealthAction : ItemAction {

    [SerializeField]
    private float HealthPointsToRestore = 100;
    [SerializeField]
    private float TimeToRestore = 2;

    public override string Description
    {
        get
        {
            return string.Format("{0} {1} health over {2} seconds", (HealthPointsToRestore > 0) ? "Restores" : "Drains", HealthPointsToRestore, TimeToRestore);
        }
    }

    public override void DoAction()
    {
        ActionOverTimeManager.AddFloatEntry(AddHealthToPlayer, HealthPointsToRestore, TimeToRestore);
    }
    private void AddHealthToPlayer(float amount)
    {
        Player.Instance.Health += amount;

        DamageTextManager.AddDamage(amount, DamageTypeManager.HealNormal, Player.Instance.gameObject);
    }
}
