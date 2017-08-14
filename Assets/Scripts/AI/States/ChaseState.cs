using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState.asset", menuName = "AI/States/Chase", order = Utility.CREATE_ASSET_ORDER_ID)]
public class ChaseState : AIState {

    [SerializeField]
    private float MinDistance;
        
    public override void Update()
    {
        base.Update();

        Vector3 deltaToTarget = Brain.Target.transform.position - Owner.transform.position;
        Vector3 targetPosition = Brain.Target.transform.position - deltaToTarget.normalized * MinDistance;

        Owner.MoveToPosition(targetPosition);
    }
}
