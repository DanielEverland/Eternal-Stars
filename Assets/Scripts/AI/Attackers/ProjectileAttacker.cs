using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileAttacker.asset", menuName = "AI/Attackers/Projectile", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ProjectileAttacker : AIAttacker {

    [SerializeField]
    private float minRange;
    [SerializeField]
    private float firingInterval = 0.2f;
    [SerializeField]
    private string projectileKey;    

    private float lastFired;

    public override void PollAttack()
    {
        if(Vector3.Distance(Owner.transform.position, Brain.Target.transform.position) <= minRange && Time.time - lastFired >= firingInterval)
        {
            DoAttack();
        }
    }
    protected override void DoAttack()
    {
        lastFired = Time.time;

        Projectile projectile = PlayModeObjectPool.Pool.GetObject(projectileKey).GetComponent<Projectile>();
        projectile.InitializePosition(Owner.transform.position, Brain.Target.transform.position);
    }
}
